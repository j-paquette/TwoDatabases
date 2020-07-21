using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Oracle.DataAccess.Client;
using System.Data.OleDb;


namespace TwoDatabases
{
    public class DbUtilities
    {
        public static ConnectionStringSettings GetConnectionString(string server, string database)
        {
            string connStr = ConfigurationManager.ConnectionStrings["xe_josee"].ToString();
            connStr = string.Format(connStr, server, database);

            return new ConnectionStringSettings("xe_josee", connStr);

        }


    }
}
