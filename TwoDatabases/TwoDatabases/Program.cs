using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace TwoDatabases
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionStringJosee = "user id=josee;password=clientList1;data source=oracle";

            DbProviderFactory factory =
                DbProviderFactories.GetFactory("Oracle.ManagedDataAccess.Client");

            DbConnection connection = factory.CreateConnection();

            try
            {
                connection.ConnectionString = connectionStringJosee;
                connection.Open();

                DbCommand cmd = factory.CreateCommand();
                cmd.Connection = connection;

                //cmd.CommandText = "select * from trend";
                cmd.CommandText =
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

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine(reader["trend_service_account_id"] + " : " + reader["trend_service_account_code"] + " : " + reader["service_name"] + " : " + reader["Transactions"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
