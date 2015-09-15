using EPMCS.DAL;
using EPMCS.Service.Conf;
using EPMCS.Service.Util;
using log4net;
using Quartz;
using System;
using System.Linq;
using System.Reflection;

namespace EPMCS.Service.Job
{
    public class CollectJobListener : IJobListener
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string Name
        {
            get { return "collect_job_listener"; }
        }

        public void JobToBeExecuted(Quartz.IJobExecutionContext context)
        {
            //throw new NotImplementedException();
            logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>JobToBeExecuted");
        }

        public void JobExecutionVetoed(Quartz.IJobExecutionContext context)
        {
            //throw new NotImplementedException();
            logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>JobExecutionVetoed");
        }

        public void JobWasExecuted(Quartz.IJobExecutionContext context, Quartz.JobExecutionException jobException)
        {
            logger.Debug(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>CollectJobListener.JobWasExecuted");
            using (MysqlDbContext dbcontext = new MysqlDbContext())
            {
                int count = dbcontext.Datas.Count(m => m.ValueLevel > -1);
                logger.DebugFormat("*************************数据库中共有{0}条采集的数据****************************************", count);
            }
            if (context.JobDetail.JobDataMap.ContainsKey(Consts.AlarmLevelKey))
            {
                int level = context.JobDetail.JobDataMap.GetInt(Consts.AlarmLevelKey);
                logger.DebugFormat("当前组的最大报警级别={0}", level);
                setAlarm(level);
            }
            else
            {
                logger.InfoFormat("当前组的最大报警级别={0}", "None");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"> 1-4 : 亮对应报警灯, 0 : 全灭 ,-1 全亮</param>
        private void setAlarm(int level)
        {
            string alarmPort = ConfUtil.AlarmSerialPort();
            if (alarmPort != null && alarmPort.ToUpper().StartsWith("COM"))
            {
                short[] d = new short[] { 0, 0, 0, 0 };
                if (level > 0)
                {
                    d[level - 1] = 1;
                }
                else if (level == -1)//全亮
                {
                    d = new short[] { 1, 1, 1, 1 };
                }
                else
                {
                    //全灭
                }
                ModbusPoll mp = new ModbusPoll();
                try
                {
                    mp.StartPoll(ConfUtil.AlarmSerialPort(), d);
                }
                catch (Exception ex)
                {
                    logger.Error("访问报警设备异常", ex);
                }
                finally
                {
                    mp.StopPoll();
                }
            }
            else
            {
                logger.Debug("没有发现报警串口,不报警!");
            }
        }
    }
}

//0x01,0x10,0x03,0x00,0x00,0x01,0x02,0x00,0x00,0x95,0x50
//768 - 771

//485接线问题: 485母口1接电表485负, 485母口2届电表485正