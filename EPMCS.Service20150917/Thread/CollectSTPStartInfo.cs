using Amib.Threading;
using log4net;
using System.Reflection;

namespace EPMCS.Service.Thread
{
    public class CollectSTPStartInfo : BaseSTPStartInfo
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CollectSTPStartInfo()
            : base()
        {
            base.PostExecuteWorkItemCallback = PostExecuteWorkItemCallback;
        }

        private static void PostExecuteWorkItemCallback(IWorkItemResult wir)
        {
            //TODO 线程结束时调用
            logger.Debug("<<<采集线程结束>>>");
        }
    }
}