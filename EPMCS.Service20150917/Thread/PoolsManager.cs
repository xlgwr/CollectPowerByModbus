using Amib.Threading;
using System;

namespace EPMCS.Service.Thread
{
    public class PoolsManager
    {
        private volatile static SmartThreadPool collectDataThreadPool;
        private static readonly object collectDataThreadPoolLocker = new object();
        public static STPStartInfo collectDataThreadPoolStartInfo;

        public static SmartThreadPool GetCollectDataThreadPoolInstance()
        {
            if (collectDataThreadPool == null)
            {
                lock (collectDataThreadPoolLocker)
                {
                    if (collectDataThreadPool == null)
                    {
                        if (collectDataThreadPoolStartInfo == null)
                        {
                            throw new Exception("线程池需要启动参数");
                        }
                        collectDataThreadPool = new SmartThreadPool(collectDataThreadPoolStartInfo);
                        collectDataThreadPool.Start();
                    }
                }
            }
            return collectDataThreadPool;
        }

        private volatile static SmartThreadPool uploadDataThreadPool;
        private static readonly object uploadDataThreadPoolLocker = new object();
        public static STPStartInfo uploadDataThreadPoolStartInfo;

        public static SmartThreadPool GetUploadDataThreadPoolInstance()
        {
            if (uploadDataThreadPool == null)
            {
                lock (uploadDataThreadPoolLocker)
                {
                    if (uploadDataThreadPool == null)
                    {
                        if (uploadDataThreadPoolStartInfo == null)
                        {
                            throw new Exception("线程池需要启动参数");
                        }
                        uploadDataThreadPool = new SmartThreadPool(uploadDataThreadPoolStartInfo);
                        uploadDataThreadPool.Start();
                    }
                }
            }
            return uploadDataThreadPool;
        }
    }
}