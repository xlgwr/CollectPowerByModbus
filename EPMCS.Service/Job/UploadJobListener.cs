using EPMCS.DAL;
using EPMCS.Model;
using EPMCS.Service.Conf;
using EPMCS.Service.Entity;
using EPMCS.Service.Util;
using log4net;
using Quartz;
using System;
using System.Linq;
using System.Reflection;

namespace EPMCS.Service.Job
{
    public class UploadJobListener : IJobListener
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string Name
        {
            get { return "upload_job_listener"; }
        }

        public void JobToBeExecuted(Quartz.IJobExecutionContext context)
        {
            logger.Debug("UploadJobListener.JobToBeExecuted() 执行之前先检查一下表是否需要更新!!");
            string Url = ConfUtil.UploadUrl() + "?customerId=" + ConfUtil.CustomerId() + "&Version=" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try
            {
                logger.DebugFormat("上传空数据,以获得表的最后更新时间!");
                DataResult ret = HttpClientHelper.PostResponse<DataResult>(Url, "[]");
                if (ret != null)
                {
                    logger.DebugFormat("表的最后更新时间 msec={0}", ret.DeviceLatestUpdateMsec);
                    Common.UpdateMeters(ret.DeviceLatestUpdateMsec);
                    context.JobDetail.JobDataMap.Put(Consts.DeviceLatestUpdateKey, true);
                }
            }
            catch (Exception ex)
            {
                context.JobDetail.JobDataMap.Put(Consts.DeviceLatestUpdateKey, false);
                logger.Debug("获得表的最后更新时间异常", ex);
            }
        }

        public void JobExecutionVetoed(Quartz.IJobExecutionContext context)
        {
            // throw new NotImplementedException();
        }

        public void JobWasExecuted(Quartz.IJobExecutionContext context, Quartz.JobExecutionException jobException)
        {
            logger.Debug("UploadJob.Execution() 已经完成!!");
            //if (context.JobDetail.JobDataMap.ContainsKey(Consts.DeviceLatestUpdateKey))
            //{
            //    long msec = context.JobDetail.JobDataMap.GetLong(Consts.DeviceLatestUpdateKey);
            //    updateMeters(msec);
            //}
            //else
            //{
            //    logger.DebugFormat("UploadJobListener.JobWasExecuted()没有找到KEY : {0}", Consts.DeviceLatestUpdateKey);
            //}
        }


    }
}