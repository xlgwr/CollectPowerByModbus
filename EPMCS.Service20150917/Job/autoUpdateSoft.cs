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
            var updater = EPMCS.Service.Service.updater;
            logger.Debug("执行软件升级任务!!!!!!!!!!!!!!!");
            updater.Error += (s, e) =>
              {
                  logger.DebugFormat("更新发生了错误：{0},URL:{1}", updater.Context.Exception.Message, updater.Context.UpdateInfoFileUrl);
              };
            updater.UpdatesFound += (s, e) =>
              {
                  logger.Debug("发现了新版本： " + updater.Context.UpdateInfo.AppVersion);
                 //开始更新
                  updater.StartExternalUpdater();
              };
            updater.NoUpdatesFound += (s, e) =>
               {
                   logger.Debug("没有新版本！ ");
               };
            updater.MinmumVersionRequired += (s, e) =>
              {
                  logger.Debug("当前版本过低无法使用自动更新！ ");
              };
            //Updater.CheckUpdateSimple();//
            updater.Context.EnableEmbedDialog = false;

            updater.BeginCheckUpdateInProcess();

        }
    }
}