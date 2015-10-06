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
                try
                {
                    logger.DebugFormat("开始清除数据!");
                    dbcontext.Database.ExecuteSqlCommand("delete from uploaddatas where uploaded={0}", 3);
                    dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.Error("清除数据失败", ex);
                }
            }

            using (MysqlDbContext dbcontext = new MysqlDbContext())
            {
                //清理长时间未上传成功的数据
                int days = ConfUtil.ForceClearDays();
                logger.DebugFormat("采集后超过{0}天的数据强制清除(未上传的数据也清除)!", days);
                try
                {
                    long nowJavaMill=(long)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                    long daysMill =(long)TimeSpan.FromDays(days).TotalMilliseconds;
                    dbcontext.Database.ExecuteSqlCommand("delete from uploaddatas where  {0} - groupstamp > {1} ", nowJavaMill, daysMill);
                    dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.Error("强制清除失败", ex);
                }
            }
        }
    }
}