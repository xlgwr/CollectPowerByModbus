using EPMCS.DAL;
using EPMCS.Model;
using EPMCS.Service.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using EPMCS.Service.Entity;
using EPMCS.Service.Util;

namespace EPMCS.Service.Job
{
    public class Common
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 根据提供的时间,检查表是否有更新,有则更新表数据
        /// </summary>
        /// <param name="msec">从服务器获取的最后更新表的时间(java日期毫秒数1970-1-01 00:00:00.000)</param>
        public static void UpdateMeters(string msec)
        {
            if (!String.IsNullOrEmpty(msec))
            {
                try
                {
                    using (MysqlDbContext dbcontext = new MysqlDbContext())
                    {
                        KeyValParam p = dbcontext.Params.FirstOrDefault(m => m.K == Consts.DeviceLatestUpdateKey);
                        logger.DebugFormat("收到表最后更新时间: {0},数据库存储时间: {1}", msec, p==null?"":p.V);
                        if (p == null || msec !=  p.V)
                        {
                            try
                            {
                                string Url = ConfUtil.MetersUrl() + "?customerId=" + ConfUtil.CustomerId();
                                logger.DebugFormat("Common.UpdateMeters(...)取表URL : {0}", Url);
                                MeterResult ret = HttpClientHelper.GetResponse<MeterResult>(Url);
                                if (ret != null && ret.Data != null && ret.Data.Count > 0)
                                {
                                    dbcontext.Meters.RemoveRange(dbcontext.Meters);
                                    dbcontext.Meters.AddRange(ret.Data);
                                    if (p == null)
                                    {
                                        p = new KeyValParam { K = Consts.DeviceLatestUpdateKey, V = msec.Trim() };
                                        dbcontext.Params.Add(p);
                                    }
                                    else
                                    {
                                        p.V = msec.Trim();
                                    }
                                    dbcontext.SaveChanges();

                                    ConfUtil.ReloadMeters();
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Debug("同步表数据异常", ex);
                            }
                        }//no param or not equal
                    }//end using db
                }
                catch (Exception ex)
                {
                    logger.Debug("同步表数据异常2", ex);
                }
            }
        }
    }
}
