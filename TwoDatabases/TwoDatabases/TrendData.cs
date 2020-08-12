using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Types;
using TwoDatabases.Entities;

namespace TwoDatabases
{
    public class TrendData
    {
        public static List<TrendRecord> GetTrendRecordList(string connectString)
        {
            List<TrendRecord> trendList = new List<TrendRecord>();

            //Use the command to display the results from the Trend table
            string query =
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

            using (OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionStringByName("xe_josee")))
            //using (OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionString(connectString)))
            {
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        //what does BindByName mean?? What is it used for??
                        //cmd.BindByName = true;

                        using(OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrendRecord trend;
                                
                                trend = trendList.Find(n => n.TrendServiceAcctId == reader.GetInt32(0));

                                trend = new TrendRecord()
                                {
                                    TrendServiceAcctId = reader.GetInt32(0),
                                    TrendServiceAcctCode = reader.GetString(1),
                                    ServiceName = reader.GetString(2),
                                    Transactions = reader.GetInt32(3)
                                };
                                trendList.Add(trend);
                                //reader.Dispose();
                            }
                        }
                        //Execute the command and use DataReader to display the data
                        //Console.WriteLine();

                        //Reset OracleCommand for next job
                        //cmd.Parameters.Clear();
                        //cmd.BindByName = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadLine();
                }
            }
            return trendList;
        }

    }
}
