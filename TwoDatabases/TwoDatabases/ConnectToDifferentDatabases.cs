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
                    Console.WriteLine();
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

        public static void GetDataFromOpenOneCursor()
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
                        cursCmd.Parameters.Add("TRENDCURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;

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
                    Console.ReadLine();
                }
            }
        }

        public static void GetCursorFunction()
        {
            string conString = DbUtilitiesSecondTry.GetDifferentConnectionString("xe_josee");
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from the EMPLOYEES table
                        //cmd.CommandText = "select first_name from employees where department_id = :id";

                        //Assign id to the department number 20 
                        //OracleParameter id = new OracleParameter("id", 20);
                        //cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        //while (reader.Read())
                        //{
                        //    Console.WriteLine("Employee First Name: " + reader.GetString(0));
                        //}

                        //id.Dispose();
                        //reader.Dispose();
                        //Console.WriteLine();

                        //Demo: Batch SQL and REF Cursors
                        // Anonymous PL/SQL block embedded in code - executes in one DB round trip

                        //Reset OracleCommand for use in next demo
                        //cmd.Parameters.Clear();
                        //cmd.BindByName = false;

                        cmd.CommandText = 
                        "DECLARE  "
                        + "BEGIN " 
                        + "OPEN for "
                        + "SELECT cd_tsa.trend_service_account_id, "
                        + "cd_tsa.trend_service_account_code, "
                        + "t.service_name, "
                        + "COUNT(t.audit_transaction_id) Transactions "
                        + "FROM cd_trend_service_account cd_tsa "
                        + "inner join trend t "
                        + "on cd_tsa.trend_service_account_id = t.trend_service_account_id "
                        + "GROUP BY cd_tsa.TREND_SERVICE_ACCOUNT_ID, "
                        + "cd_tsa.trend_service_account_code, "
                        + "t.service_name "
                        + "END;";

                        cmd.CommandType = CommandType.Text;

                        //ODP.NET has native Oracle data types, such as Oracle REF 
                        // Cursors, which can be mapped to .NET data types

                        //Bind REF Cursor Parameters for each department
                        //Select statement
                        OracleParameter p1 = cmd.Parameters.Add("refcursor1",
                          OracleDbType.RefCursor);
                        p1.Direction = ParameterDirection.Output;

                        //Select employees in department 20
                        //OracleParameter p2 = cmd.Parameters.Add("refcursor2",
                        // OracleDbType.RefCursor);
                        //p2.Direction = ParameterDirection.Output;

                        //Select employees in department 30
                        //OracleParameter p3 = cmd.Parameters.Add("refcursor3",
                        // OracleDbType.RefCursor);
                        //p3.Direction = ParameterDirection.Output;

                        //Execute batched statement
                        cmd.ExecuteNonQuery();

                        //Let's retrieve the three result sets with DataReaders
                        OracleDataReader dr1 =
                          ((OracleRefCursor)cmd.Parameters[0].Value).GetDataReader();
                        //OracleDataReader dr2 =
                        //  ((OracleRefCursor)cmd.Parameters[1].Value).GetDataReader();
                        //OracleDataReader dr3 =
                        //  ((OracleRefCursor)cmd.Parameters[2].Value).GetDataReader();

                        //Let's retrieve the results from the DataReaders
                        while (dr1.Read())
                        {
                            Console.WriteLine("trend_service_account_id: " + dr1.GetInt64(0) + ", " +
                              "trend_service_account_code:" + dr1.GetString(1) + ", " +
                              "service_name: " + dr1.GetString(2) + ", " +
                              "Transactions: " + dr1.GetInt32(3));
                        }
                        Console.WriteLine();

                        //while (dr2.Read())
                        //{
                        //    Console.WriteLine("Employee Name: " + dr2.GetString(0) + ", " +
                        //      "Employee Dept:" + dr2.GetDecimal(1));
                        //}
                        //Console.WriteLine();

                        //while (dr3.Read())
                        //{
                        //    Console.WriteLine("Employee Name: " + dr3.GetString(0) + ", " +
                        //      "Employee Dept:" + dr3.GetDecimal(1));
                        //}

                        //Clean up
                        p1.Dispose();
                        //p2.Dispose();
                        //p3.Dispose();
                        dr1.Dispose();
                        //dr2.Dispose();
                        //dr3.Dispose();

                        Console.WriteLine("Press 'Enter' to continue");
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
