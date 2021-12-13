using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net.Util;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace LearningCSharp
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string Log4netPath { get; set; }
        static void Main(string[] args)
        {
            InitializeLogConfiguration();
            Logger.Debug("This is our first debug log");
            Console.WriteLine("Hello World!");
        }

        static void InitializeLogConfiguration()
        {
            try
            {
                var log4netConfig = new XmlDocument();
                var str = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "log4net.config");
                if (File.Exists(str))
                {
                    Log4netPath = str;
                    log4netConfig.Load(File.OpenRead(Log4netPath));
                    var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
                    XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
                    SystemInfo.NullText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message:{ex.Message}, Exception Detail:{ex.StackTrace}");
            }
        }
    }
}
