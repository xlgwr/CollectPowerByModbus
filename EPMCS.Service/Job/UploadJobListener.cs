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
            // throw new NotImplementedException();
        }

        public void JobExecutionVetoed(Quartz.IJobExecutionContext context)
        {
            // throw new NotImplementedException();
        }

        public void JobWasExecuted(Quartz.IJobExecutionContext context, Quartz.JobExecutionException jobException)
        {
            logger.Debug("UploadJob.Execution() 已经完成!!");
            if (context.JobDetail.JobDataMap.ContainsKey(Consts.DeviceLatestUpdateKey))
            {
                long msec = context.JobDetail.JobDataMap.GetLong(Consts.DeviceLatestUpdateKey);
                if (msec > 0)
                {
                    using (MysqlDbContext dbcontext = new MysqlDbContext())
                    {
                        KeyValParam p = dbcontext.Params.FirstOrDefault(m => m.K == Consts.DeviceLatestUpdateKey);
                        long dbmsec = 0;
                        if (!(p != null && long.TryParse(p.V, out dbmsec)))
                        {
                            dbmsec = 0;
                        }

                        logger.DebugFormat("收到表最后更新时间 {0},数据库存储时间: {1}", msec, dbmsec);
                        if (msec != dbmsec)
                        {
                            try
                            {
                                string Url = ConfUtil.MetersUrl() + "?customerId=" + ConfUtil.CustomerId();
                                logger.DebugFormat("UploadJobListener.JobWasExecuted()取表URL : {0}", Url);
                                MeterResult ret = HttpClientHelper.GetResponse<MeterResult>(Url);
                                if (ret != null && ret.Data != null && ret.Data.Count > 0)
                                {
                                    dbcontext.Meters.RemoveRange(dbcontext.Meters);
                                    dbcontext.Meters.AddRange(ret.Data);
                                    if (p == null)
                                    {
                                        p = new KeyValParam { K = Consts.DeviceLatestUpdateKey, V = msec.ToString() };
                                        dbcontext.Params.Add(p);
                                    }
                                    else
                                    {
                                        p.V = msec.ToString();
                                    }
                                    dbcontext.SaveChanges();

                                    ConfUtil.ReloadMeters();
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Debug("同步表数据异常", ex);
                            }
                        }
                    }
                }
            }
            else
            {
                logger.DebugFormat("UploadJobListener.JobWasExecuted()没有找到KEY : {0}", Consts.DeviceLatestUpdateKey);
            }
        }
    }
}