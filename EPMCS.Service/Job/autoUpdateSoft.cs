using EPMCS.DAL;
using EPMCS.Service.Conf;
using log4net;
using Quartz;
using System;
using System.Linq;
using System.Reflection;
using FSLib.App.SimpleUpdater;

namespace EPMCS.Service.Job
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class autoUpdateSoftJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(Quartz.IJobExecutionContext context)
        {
            logger.Debug("执行软件升级任务!!!!!!!!!!!!!!!");
            EPMCS.Service.Service.updater.Error += (s, e) =>
             {
                 logger.DebugFormat("更新发生了错误：{0},URL:{1}", EPMCS.Service.Service.updater.Context.Exception.Message, EPMCS.Service.Service.updateurl);
             };
            EPMCS.Service.Service.updater.UpdatesFound += (s, e) =>
             {
                 logger.Debug("发现了新版本： " + EPMCS.Service.Service.updater.Context.UpdateInfo.AppVersion);
             };
            EPMCS.Service.Service.updater.NoUpdatesFound += (s, e) =>
             {
                 logger.Debug("没有新版本！ ");
             };
            EPMCS.Service.Service.updater.MinmumVersionRequired += (s, e) =>
             {
                 logger.Debug("当前版本过低无法使用自动更新！ ");
             };
            Updater.CheckUpdateSimple();
        }
    }
}