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
    public class ServiceAccountClientDetailsData
    {
        public static List<ServiceAcctClientDetailsRecord> GetServiceClientDetailsList(string connectString)
        {
            List<ServiceAcctClientDetailsRecord> ServiceClientDetailsList = new List<ServiceAcctClientDetailsRecord>();

            //Use the command to display the results from the Trend table
            string query =
            "SELECT sacd.service_account_id, "
            + "sacd.service_account_code, "
            + "sacd.CSD_URL, sacd.contact_email "
            + "FROM service_account_client_details sacd ";

            using (OracleConnection con = new OracleConnection(DbUtilitiesSecondTry.GetDifferentConnectionStringByName("xe_user_client_details")))
            {
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        //what does BindByName mean?? What is it used for??
                        //cmd.BindByName = true;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ServiceAcctClientDetailsRecord client;

                                client = ServiceClientDetailsList.Find(n => n.ServiceAcctID == reader.GetInt32(0));

                                client = new ServiceAcctClientDetailsRecord()
                                {
                                    ServiceAcctID = reader.GetInt32(0),
                                    ServiceAcctCode = reader.GetString(1),
                                    CsdUrl = reader.GetString(2),
                                    ContactEmail = reader.GetString(3)
                                };
                                ServiceClientDetailsList.Add(client);
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
            return ServiceClientDetailsList;
        }
    }
}
