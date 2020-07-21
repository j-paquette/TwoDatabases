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

namespace TwoDatabases
{
    public class Program
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine(DbUtilities.GetConnectionString("localhost", "xe_josee"));

            //ConnectionStringSettings connStr = DbUtilities.GetConnectionString();

            //Console.WriteLine(DbConnectToJosee.GetTrendData());

            //Console.WriteLine(DbConnectToJosee.GetTrendData(DbUtilities.GetConnectionString("xe_josee", connStr)));
        }
    }
}
