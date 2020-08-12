using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data.OleDb;
using TwoDatabases.Entities;

namespace TwoDatabases
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DbUtilitiesSecondTry.GetDifferentConnectionStringByName("xe_josee"));

            List<TrendRecord> trendRecords = TrendData.GetTrendRecordList(DbUtilitiesSecondTry.GetDifferentConnectionStringByName("xe_josee"));

            //get the total# of records in the Trend table
            Console.WriteLine($"The total # Trend records: {trendRecords.Count}");

            foreach(TrendRecord record in trendRecords)
            {
                Console.WriteLine($"{record.TrendServiceAcctId}, {record.TrendServiceAcctCode}, {record.ServiceName}, {record.Transactions}");
            }

            //Get records from Service_account_client_details table
            Console.WriteLine(DbUtilitiesSecondTry.GetDifferentConnectionStringByName("xe_user_client_details"));

            List<ServiceAcctClientDetailsRecord> ServiceClientDetailsRecords = ServiceAccountClientDetailsData.GetServiceClientDetailsList(DbUtilitiesSecondTry.GetDifferentConnectionStringByName("xe_user_client_details"));

            //get the total# of records in the Service_account_client_details table
            Console.WriteLine($"The total # Service_client_details records: {ServiceClientDetailsRecords.Count}");

            foreach (ServiceAcctClientDetailsRecord record in ServiceClientDetailsRecords)
            {
                Console.WriteLine($"{record.ServiceAcctID}, {record.ServiceAcctCode}, {record.CsdUrl}, {record.ContactEmail}");
            }


        }
    }
}
