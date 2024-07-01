using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db
{
    public class DbManager
    {
        private string connectionString = string.Empty;
        public string ConnectionString { get => connectionString; private set => connectionString = value; }

        public DbManager(string connectionName)
        {

            var conStr = ConfigurationManager.ConnectionStrings[connectionName];
            if(conStr == null)
                throw new ArgumentException($"Строка подключения {connectionName} не найдена");
            ConnectionString = conStr.ConnectionString;
            
        }

        /// <summary>
        /// Создает и возвращает открытое подключение к базе данных
        /// </summary>
        /// <returns>Открытое подключение</returns>
        public SqlConnection GetNewConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

    }
}
