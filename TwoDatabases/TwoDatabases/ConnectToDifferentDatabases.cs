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
    public class ConnectToDifferentDatabases
    {
        public void GetDataFromTrend()
        {
            //OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionString("xe_josee"));


            using (OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionString("xe_josee")))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display the results from the Trend table
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

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }

                        reader.Dispose();
                        Console.WriteLine();

                        //Reset OracleCommand for next job
                        cmd.Parameters.Clear();
                        cmd.BindByName = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadLine();
                }
            }
        }

        public void GetDataFromServiceAccountClientDetails()
        {
            using (OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionString("xe_josee")))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display the results from the Trend table
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

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }

                        reader.Dispose();
                        Console.WriteLine();

                        //Reset OracleCommand for next job
                        cmd.Parameters.Clear();
                        cmd.BindByName = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadLine();
                }
            }
        }
    }
}
