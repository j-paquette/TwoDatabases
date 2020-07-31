using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess;
using System.Data.OleDb;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using TwoDatabases.Entities;

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
            using (OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionString("xe_user_client_details")))
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

        //public void GetFromTrend(string connectionString)
        //{
        //    System.Data.OracleClient.OracleConnection con = new System.Data.OracleClient.OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionString("xe_josee"));

        //    con.Open();

        //    System.Data.OracleClient.OracleCommand cmd = con.CreateCommand();

        //    try
        //    {
        //        //Use the command to display the results from the Trend table
        //        cmd.CommandText =
        //        "SELECT cd_tsa.trend_service_account_id, "
        //        + "cd_tsa.trend_service_account_code, "
        //        + "t.service_name, "
        //        + "COUNT(t.audit_transaction_id) Transactions "
        //        + "FROM cd_trend_service_account cd_tsa "
        //        + "inner join trend t "
        //        + "on cd_tsa.trend_service_account_id = t.trend_service_account_id "
        //        + "where t.audit_transaction_date_created >= add_months(sysdate, :TransactionMaxDays) "
        //        + "GROUP BY cd_tsa.TREND_SERVICE_ACCOUNT_ID, "
        //        + "cd_tsa.trend_service_account_code, "
        //        + "t.service_name  ";

        //        System.Data.OracleClient.OracleDataReader reader = cmd.ExecuteReader();
        //        reader.Read();
        //        //using the Oracle specific getters for each type is faster 
        //        //using GetOracleValue.
        //        //First column, trend_service_account_id, is a number datatype and maps to OracleNumber1.
        //        OracleNumber oracleNumber1 = reader.GetOracleNumber(0);
        //        Console.WriteLine("trend_service_account_id " + oracleNumber1.ToString());

        //        //Second column, trend_service_account_code, is a varchar2 datatype and maps to OracleString.
        //        System.Data.OracleClient.OracleString oracleString1 = reader.GetOracleString(1);
        //        Console.WriteLine("trend_service_account_code " + oracleString1.ToString());

        //        //Third column, service_name, is a varchar2 datatype and maps to OracleString.
        //        System.Data.OracleClient.OracleString oracleString2 = reader.GetOracleString(2);
        //        Console.WriteLine("service_name " + oracleString2.ToString());

        //        //Fourth column, audit_transaction_id, is a Number datatype and maps to OracleString.
        //        OracleNumber oracleNumber2 = reader.GetOracleNumber(3);
        //        Console.WriteLine("audit_transaction_id " + oracleNumber2.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //}

        public void GetDataFromOpenOneCursor()
        {
            using (OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionString("xe_josee")))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        OracleCommand cursCmd = new OracleCommand("CURSPKG.OPEN_ONE_CURSOR", con);
                        cursCmd.CommandType = CommandType.StoredProcedure;
                        cursCmd.Parameters.Add("TRENDCURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        OracleDataReader reader = cursCmd.ExecuteReader();

                        Console.WriteLine("\nService Acct ID\tService Acct Code\tService Acct Name\tTransactions");

                        while (reader.Read()) Console.WriteLine("{0}\t{1}, {2}", reader.GetOracleValue(0), reader.GetOracleString(1), reader.GetOracleString(2), reader.GetOracleString(3));

                        reader.NextResult();

                        reader.Close();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }
    }

}
