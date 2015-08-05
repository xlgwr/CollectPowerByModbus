using Amib.Threading;
using log4net;
using System.Reflection;

namespace EPMCS.Service.Thread
{
    public class UploadSTPStartInfo : BaseSTPStartInfo
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UploadSTPStartInfo()
            : base()
        {
            base.PostExecuteWorkItemCallback = PostExecuteWorkItemCallback;
        }

        private static void PostExecuteWorkItemCallback(IWorkItemResult wir)
        {
            //TODO 线程结束时调用
            logger.Debug("上传线程结束");
        }
    }
}