using EPMCS.DAL;
using EPMCS.Model;
using EPMCS.Service.Conf;
using EPMCS.Service.Entity;
using EPMCS.Service.Util;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EPMCS.Service.Job
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class UploadJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(IJobExecutionContext context)
        {
            string Url = ConfUtil.UploadUrl() + "?customerId=" + ConfUtil.CustomerId();
            logger.DebugFormat("执行上传任务!!!!!!!!!!!!!!! URL= {0}", Url);
            int UploadRowsCount = 0;
            try
            {
                using (MysqlDbContext dbcontext = new MysqlDbContext())
                {
                    //change by xlg, flag: 0->未使用/未上传，1->未使用/已上传，2->使用/未上传，3->使用/上传，
                    //var groups = dbcontext.Datas.Where(m => (m.Uploaded == 0 || m.Uploaded == 2)).Distinct();
                    var tmpUploadSQLQuery = dbcontext.Datas.Where(m => (m.Uploaded == 0 || m.Uploaded == 2)).ToList();
                    var groups = tmpUploadSQLQuery.Select(m => m.Groupstamp).Distinct();

                    foreach (var g in groups)
                    {
                        //add by xlg, flag: 0->未使用/未上传，1->未使用/已上传，2->使用/未上传，3->使用/上传，
                        //List<UploadData> data = dbcontext.Datas.Where(m => m.Groupstamp == g && m.Uploaded == 0).ToList();
                        var data = tmpUploadSQLQuery.Where(m => m.Groupstamp.Equals(g)).ToList();
                        UploadRowsCount += data.Count;
                        string sendStr = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                        logger.DebugFormat("执行上传任务!!!!!!!!!!!!!!! 发送上传数据 = {0}", sendStr);
                        int retCount = 0;
                        DataResult ret = HttpClientHelper.PostResponse<DataResult>(Url, sendStr);
                        if (ret != null)
                        {
                            ret.DeviceLatestUpdateMsec = ret.DeviceLatestUpdateMsec ?? "0";
                            long msec = 0;
                            if (long.TryParse(ret.DeviceLatestUpdateMsec, out msec) && msec > 1)
                            {
                                context.JobDetail.JobDataMap.Put(Consts.DeviceLatestUpdateKey, msec);
                            }
                        }
                        if (ret.Status == 1 && ret.Customer != null && ret.Customer.Count > 0)
                        {
                            foreach (var custm in ret.Customer)
                            {
                                if (custm.Devices != null && custm.Devices.Count > 0)
                                {
                                    foreach (var deviceData in custm.Devices)
                                    {
                                        if (deviceData.Data != null && deviceData.Data.Count > 0)
                                        {
                                            foreach (var row in deviceData.Data)
                                            {
                                                retCount++;

                                                UploadData one = data.FirstOrDefault(m => m.CustomerId == custm.CustomerId && m.DeviceId == deviceData.DeviceId && m.Groupstamp == row.Groupstamp && row.Status == 1);
                                                if (one != null)
                                                {
                                                    logger.DebugFormat("#######找到 CustomerId={0} ,DeviceId={1} , Groupstamp={2} #########", deviceData.DeviceId, deviceData.DeviceId, row.Groupstamp);
                                                    //add by xlg.   one.Uploaded = 1;
                                                    one.Uploaded = one.Uploaded == 0 ? 1 : 3;
                                                }
                                                else
                                                {
                                                    logger.DebugFormat("#######没找到 CustomerId={0} ,DeviceId={1} , Groupstamp={2} #########", deviceData.DeviceId, deviceData.DeviceId, row.Groupstamp);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            dbcontext.SaveChanges();
                        }
                        logger.DebugFormat("#######一共返回{0}条记录上传结果######### {1}", retCount, g);
                    }
                }//end using
            }
            catch (Exception ex)
            {
                logger.Debug("上传异常", ex);
            }

            if (UploadRowsCount == 0)
            {
                try
                {
                    logger.DebugFormat("没有采集记录,上传空数据!");
                    DataResult ret = HttpClientHelper.PostResponse<DataResult>(Url, "[]");
                    if (ret != null && long.Parse(ret.DeviceLatestUpdateMsec) > 1)
                    {
                        long msec = 0;
                        if (long.TryParse(ret.DeviceLatestUpdateMsec, out msec) && msec > 1)
                        {
                            logger.DebugFormat("没有采集记录,上传空数据! 监听值 msec={0}", msec);
                            context.JobDetail.JobDataMap.Put(Consts.DeviceLatestUpdateKey, msec);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Debug("上传空数据异常", ex);
                }
            }
            else
            {
                logger.DebugFormat("本次一共上传{0}条数据!", UploadRowsCount);
            }
        }
    }
}