using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net.Util;
using System;
using System.Data.SqlClient;
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
            InitializeDb();
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

        static void InitializeDb()
        {
            string connectionString;
            SqlConnection _sqlConnection;
            connectionString = "Data source=WAHAB-HUSSAIN\\SQLEXPRESS;Initial Catalog=tempdb;User id=sa;password=sa;";
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
            SqlCommand _sqlCommand = new SqlCommand("select * from test;", _sqlConnection);
            SqlDataReader _sqlDataReader = _sqlCommand.ExecuteReader();
            int id;
            string name;
            if (_sqlDataReader.HasRows)
            {
                while (_sqlDataReader.Read())
                {
                    id = (int)_sqlDataReader[0];
                    name = _sqlDataReader[1].ToString();
                    Console.WriteLine($"id:{id} and name:{name}");
                }
            }
            _sqlConnection.Close();

        }

    }
}
