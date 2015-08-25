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

            bool urlOk = context.JobDetail.JobDataMap.GetBoolean(Consts.DeviceLatestUpdateKey);
            logger.DebugFormat("获取表更新时间的提交是否成功? {0}", urlOk);
            string Url = ConfUtil.UploadUrl() + "?customerId=" + ConfUtil.CustomerId();
            logger.DebugFormat("执行上传任务!!!!!!!!!!!!!!! URL= {0}", Url);
            int UploadRowsCount = 0;
            int retCount = 0;
            try
            {
                using (MysqlDbContext dbcontext = new MysqlDbContext())
                {
                    //uploaded: 0->未使用/未上传，1->未使用/已上传，2->使用/未上传，3->使用/上传，
                    var takeNum = ConfUtil.UploadedTake();
                    var fetchData = dbcontext.Datas.Where(m => (m.Uploaded & 1) == 0).OrderByDescending(m => m.PowerDate).Take(takeNum);
                    var tmpcount = 0;
                    while((tmpcount= fetchData.Count()) > 0)
                    {          
                        logger.DebugFormat("#################开始执行上传任务!!!!!!!!!!!!!!! Count：{0}", tmpcount);
                        UploadRowsCount = UploadRowsCount + tmpcount;
                        string sendJson = Newtonsoft.Json.JsonConvert.SerializeObject(fetchData.ToList());
                        logger.DebugFormat("执行上传任务!!!!!!!!!!!!!!! 发送上传数据 = {0}", sendJson);
                        DataResult ret = HttpClientHelper.PostResponse<DataResult>(Url, sendJson);
                        if (ret != null)
                        {
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
                                                    if (row.Status == 1)
                                                    {
                                                        retCount++;
                                                    }

                                                    UploadData one = dbcontext.Datas.FirstOrDefault(m => m.CustomerId == custm.CustomerId && m.DeviceId == deviceData.DeviceId && m.Groupstamp == row.Groupstamp && row.Status == 1);
                                                    if (one != null)
                                                    {
                                                        one.Uploaded = (one.Uploaded | 1);
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
                            logger.DebugFormat("表的最后更新时间 msec={0}", ret.DeviceLatestUpdateMsec);
                            Common.UpdateMeters(ret.DeviceLatestUpdateMsec);
                        }

                        //fetchData = dbcontext.Datas.Where(m => (m.Uploaded & 1) == 0).OrderByDescending(m => m.PowerDate).Take(takeNum);
                    }//end while
                }//end using
            }
            catch (Exception ex)
            {
                logger.Debug("上传异常", ex);
            }

            logger.DebugFormat("本次提交{0}条数据,成功{1}条数据!", UploadRowsCount, retCount);
        }
    }
}