//#define Dev

using System.ServiceProcess;
namespace EPMCS.Service
{
#if Dev

    internal class Program
    {
        private static void Main(string[] args)
        {
            Test test = new Test();
            test.OnStart();
        }
    }

#else
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
#endif
}