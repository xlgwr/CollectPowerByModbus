using EPMCS.DAL;
using EPMCS.Service.Conf;
using log4net;
using Quartz;
using System;
using System.Linq;
using System.Reflection;

namespace EPMCS.Service.Job
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class ClearJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(Quartz.IJobExecutionContext context)
        {
            logger.Debug("执行清理任务!!!!!!!!!!!!!!!");
            using (MysqlDbContext dbcontext = new MysqlDbContext())
            {
                var pgroups = dbcontext.Datas.Where(m => m.Uploaded == 3);//change 1 to 3, by xlg
                if (pgroups != null && pgroups.Count() > 0)
                {
                    var groups = pgroups.Select(m => m.Groupstamp).Distinct();
                    logger.DebugFormat("找到{0}个需要清除的组!", groups.Count());
                    foreach (var g in groups)
                    {
                        try
                        {
                            logger.DebugFormat("开始清除组{0}已上传的数据!", g);
                            dbcontext.Database.ExecuteSqlCommand("delete from uploaddatas where Groupstamp={0} and Uploaded={1}", g, 3);//change 1 to 3, by xlg
                            //var data = dbcontext.Datas.Where(m => m.Groupstamp == g && m.Uploaded == 1);
                            //dbcontext.Datas.RemoveRange(data);
                            dbcontext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("清除组" + g + "失败", ex);
                        }
                    }
                }
            }

            using (MysqlDbContext dbcontext = new MysqlDbContext())
            {
                //清理长时间未上传成功的数据
                int days = ConfUtil.ForceClearDays();
                logger.DebugFormat("采集后超过{0}天(服务器时间为准)未上传的数据也清除!", days);
                var groups = dbcontext.Datas.Where(m => m.Uploaded == 0).Select(m => m.Groupstamp).Distinct();
                logger.DebugFormat("找到{0}个需要强制清除的组!", groups.Count());
                foreach (var gg in groups)
                {
                    var g = long.Parse(gg);
                    if (DateTime.Now.Subtract(new DateTime(g)).TotalDays > days) //超过天数则强制删除
                    {
                        try
                        {
                            logger.DebugFormat("开始强制清除组{0}未上传的数据!", gg);
                            dbcontext.Database.ExecuteSqlCommand("delete from uploaddatas where Groupstamp={0} ", gg);
                            //var data = dbcontext.Datas.Where(m => m.Groupstamp == gg);
                            //dbcontext.Datas.RemoveRange(data);
                            dbcontext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("强制清除组" + g + "失败", ex);
                        }
                    }
                }
            }
        }
    }
}