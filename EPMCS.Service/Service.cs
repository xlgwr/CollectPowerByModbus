using EPMCS.Service.Conf;
using EPMCS.Service.Job;
using EPMCS.Service.Thread;
using log4net;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.ServiceProcess;
using System.IO;
using System.Text;
using EPMCS.Service.Basic;

namespace EPMCS.Service
{
    public partial class Service : ServiceBase
    {
        private readonly ILog logger;
        public static IScheduler scheduler;
        private readonly WinLogWirter winlogger;

        public Service()
        {
            InitializeComponent();
            winlogger = new WinLogWirter();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);   
            logger = LogManager.GetLogger(GetType());
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {

                Exception ex = e.ExceptionObject as Exception;
                winlogger.LogEvent("来自“EPMCS.Service”的全局异常。" + ex.Message + "详细信息如下："
                                    + Environment.NewLine + "［InnerException］" + ex.InnerException
                                    + Environment.NewLine + "［Source］" + ex.Source
                                    + Environment.NewLine + "［TargetSite］" + ex.TargetSite
                                    + Environment.NewLine + "［StackTrace］" + ex.StackTrace);
            }
            catch { }
        }
        protected override void OnStart(string[] args)
        {
            logger.Debug("====================以下参数修改后需重启服务生效===================");
            logger.DebugFormat("未上传成功的数据保存{0}天后强制删除!", ConfUtil.ForceClearDays());
            logger.DebugFormat("清除任务间隔{0}分钟循环执行!", ConfUtil.ClearIntervalInMinutes());
            logger.DebugFormat("上传任务间隔{0}秒钟循环执行!", ConfUtil.UploadIntervalInSeconds());
            logger.DebugFormat("上传链接地址: {0} ", ConfUtil.UploadUrl());
            logger.DebugFormat("取表链接地址: {0} ", ConfUtil.MetersUrl());
            logger.DebugFormat("客户编号: {0} ", ConfUtil.CustomerId());
            logger.DebugFormat("报警串口: {0} ", ConfUtil.AlarmSerialPort());
            logger.Debug("================================================================");
            if (!checkkey())
            {
                return;
            }
           ComSerialPortCollection paramz= ConfUtil.GetComPortsParams();
           if (paramz == null || paramz.Count == 0)
           {
               throw new Exception("没有为串口配置参数,请为硬件所有串口配置通信参数!");
           }
            scheduler.Start();
            logger.Info("Quartz服务成功启动");

            PoolsManager.collectDataThreadPoolStartInfo = new CollectSTPStartInfo();
            PoolsManager.uploadDataThreadPoolStartInfo = new UploadSTPStartInfo();

            DateTimeOffset runTime = DateBuilder.EvenSecondDate(DateTimeOffset.Now);

            #region "clear"

            int clearInterval = ConfUtil.ClearIntervalInMinutes();
            IJobDetail clear_job = JobBuilder.Create<ClearJob>()
                .WithIdentity(Consts.ClearJob, Consts.ClearGroup)
                 .Build();

            ITrigger clear_trigger = TriggerBuilder.Create()
                .WithIdentity(Consts.ClearTrigger, Consts.ClearGroup)
                .StartAt(runTime)
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(clearInterval).RepeatForever())
                .Build();
            scheduler.ScheduleJob(clear_job, clear_trigger);

            #endregion "clear"

            #region "upload"

            int uploadInterval = ConfUtil.UploadIntervalInSeconds();
            IJobDetail upload_job = JobBuilder.Create<UploadJob>()
                .WithIdentity(Consts.UploadJob, Consts.UploadGroup)
                .Build();

            ITrigger upload_trigger = TriggerBuilder.Create()
                .WithIdentity(Consts.UploadTrigger, Consts.UploadGroup)
                .StartAt(runTime)
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(uploadInterval).RepeatForever())
                .Build();

            //Set up the listener
            UploadJobListener upload_listener = new UploadJobListener();
            IMatcher<JobKey> upload_matcher = KeyMatcher<JobKey>.KeyEquals(upload_job.Key);
            scheduler.ListenerManager.AddJobListener(upload_listener, upload_matcher);

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(upload_job, upload_trigger);

            #endregion "upload"

            #region "collect"

            IJobDetail job = JobBuilder.Create<CollectJob>()
                         .WithIdentity(Consts.CollectJob, Consts.CollectGroup)
                         .Build();
            //  job.JobDataMap.Put("meters", meters);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(Consts.CollectTrigger, Consts.CollectGroup)
                .StartAt(runTime)
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(60).RepeatForever())
                .Build();

            //Set up the listener
            IJobListener listener = new CollectJobListener();
            IMatcher<JobKey> matcher = KeyMatcher<JobKey>.KeyEquals(job.Key);
            scheduler.ListenerManager.AddJobListener(listener, matcher);

            scheduler.ScheduleJob(job, trigger);

            #endregion "collect"

            #region "autoUpdate soft"

            //IJobDetail autoUpdateSoft_job = JobBuilder.Create<autoUpdateSoftJob>()
            //    .WithIdentity("autoUpdateSoft_job", "autoUpdateSoft_group")
            //     .Build();

            //ITrigger autoUpdateSoft_trigger = TriggerBuilder.Create()
            //    .WithIdentity("autoUpdateSoft_trigger", "autoUpdateSoft_group")
            //    .StartAt(runTime)
            //    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
            //    .Build();
            //scheduler.ScheduleJob(autoUpdateSoft_job, autoUpdateSoft_trigger);
            #endregion
        }

        protected override void OnStop()
        {
            try
            {
                scheduler.Shutdown();
                logger.Info("Quartz服务成功终止");
            }
            finally { }

            try
            {
                if (PoolsManager.GetCollectDataThreadPoolInstance() != null)
                {
                    PoolsManager.GetCollectDataThreadPoolInstance().Shutdown(true, TimeSpan.FromSeconds(10));
                }
                if (PoolsManager.GetUploadDataThreadPoolInstance() != null)
                {
                    PoolsManager.GetUploadDataThreadPoolInstance().Shutdown(true, TimeSpan.FromSeconds(10));
                }
            }
            finally { }

            foreach (var em in ConfUtil.Ports().Values)
            {
                try
                {
                    em.Close();
                    em.Dispose();
                }
                finally { }
            }
        }

        protected override void OnPause()
        {
            scheduler.PauseAll();
        }

        protected override void OnContinue()
        {
            scheduler.ResumeAll();
        }


        private bool checkkey()
        {
            #region "check key for machine"
            try
            {
                var filepath=AppDomain.CurrentDomain.BaseDirectory + @"/FtdAdapter.Core.dll";
                FileInfo fi = new FileInfo(filepath);
                fi.LastWriteTime = Convert.ToDateTime("2015-07-22 12:49:24");
                var tmpkey = File.ReadAllText(filepath, Encoding.UTF8).Trim();
                if (!string.IsNullOrEmpty(tmpkey))
                {
                    var tomd5key = getInfoToMd5.getCPU();

                    tomd5key += getInfoToMd5.getSerialNumber();
                    tomd5key += "www.szisec.com";

                    var getmd5key = getInfoToMd5.MD5Encrypt(tomd5key);

                    if (!tmpkey.Equals(getmd5key))
                    {
                        logger.Debug("************************当前授权Key，与本机不配配，请重新授权************************");
                        logger.Error("************************当前授权Key，与本机不配配，请重新授权************************");
                        logger.Info("************************当前授权Key，与本机不配配，请重新授权************************");
                        return false;
                    }
                    else
                    {

                        logger.Info("************************当前授权正确************************");
                        logger.Debug("************************当前授权正确************************");
                        return true;
                    }
                }
                else
                {
                    logger.Error("************************请设置授权Key，然后重新始动************************");
                    logger.Debug("************************请设置授权Key，然后重新始动************************");
                    logger.Info("************************请设置授权Key，然后重新始动************************");
                    return false;
                }
            }
            catch (Exception ex)
            {

                logger.ErrorFormat("检查授权失败，请联系管理员。[{0}]", ex.Message);
                logger.DebugFormat("检查授权失败，请联系管理员。[{0}]", ex.Message);
                logger.InfoFormat("检查授权失败，请联系管理员。[{0}]", ex.Message);
                return false;
            }

            #endregion
        }
    }
}