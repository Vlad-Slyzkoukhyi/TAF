using log4net.Config;
using log4net;

namespace TAF_Task_API.Utils
{
    public class LoggerInitializer
    {
        public static void Initialize()
        {
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
        }
    }
}
