using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data.OleDb;
using TwoDatabases.Entities;

namespace TwoDatabases
{
    public class DbConnectToJosee
    {
        public static string GetTrendData(string connectionString)
        {
            //List<TrendRecords> trendTransactions = new List<TrendRecords>();

            OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionStringByName("xe_josee"));

            string queryString =
            "SELECT cd_tsa.trend_service_account_id, "
            + "cd_tsa.trend_service_account_code, "
            + "t.service_name, "
            + "COUNT(t.audit_transaction_id) Transactions "
            + "FROM cd_trend_service_account cd_tsa "
            + "inner join trend t "
            + "on cd_tsa.trend_service_account_id = t.trend_service_account_id "
            + "where t.audit_transaction_date_created >= add_months(sysdate, :TransactionMaxDays) "
            + "GROUP BY cd_tsa.TREND_SERVICE_ACCOUNT_ID, "
            + "cd_tsa.trend_service_account_code, "
            + "t.service_name  ";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetInt32(1) + ", " + reader.GetString(2) + ", "
                        + reader.GetInt32(3));
                }

                reader.Close();
            }
            return connectionString;
        }


        

    }
}
