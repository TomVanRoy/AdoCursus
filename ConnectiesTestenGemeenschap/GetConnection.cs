using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace ConnectiesTestenGemeenschap
{
    public class GetConnection
    {
        private static ConnectionStringSettings conSetting = ConfigurationManager.ConnectionStrings["Tuincentrum"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conSetting.ProviderName);

        public DbConnection GetConnected()
        {
            var connection = factory.CreateConnection();
            connection.ConnectionString = conSetting.ConnectionString;
            return connection;
        }
    }
}