using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace AdoGemeenschap
{
    public class BankDbManager
    {
        private static ConnectionStringSettings conBankSetting = ConfigurationManager.ConnectionStrings["Bank"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBankSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var conBank = factory.CreateConnection();
            conBank.ConnectionString = conBankSetting.ConnectionString;
            return conBank;
        }
    }

    public class Bank2DbManager
    {
        private static ConnectionStringSettings conBankSetting = ConfigurationManager.ConnectionStrings["Bank2"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBankSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var conBank = factory.CreateConnection();
            conBank.ConnectionString = conBankSetting.ConnectionString;
            return conBank;
        }
    }

    public class BierenDbManager
    {
        private static ConnectionStringSettings conBierenSetting = ConfigurationManager.ConnectionStrings["Bieren"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBierenSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var conBieren = factory.CreateConnection();
            conBieren.ConnectionString = conBierenSetting.ConnectionString;
            factory.CreateCommand();
            return conBieren;
        }
    }

    public class TuinDbManager
    {
        private static ConnectionStringSettings conSetting = ConfigurationManager.ConnectionStrings["Tuincentrum"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var connection = factory.CreateConnection();
            connection.ConnectionString = conSetting.ConnectionString;
            return connection;
        }
    }
}