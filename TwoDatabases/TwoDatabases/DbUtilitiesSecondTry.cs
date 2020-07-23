using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDatabases
{
    public class DbUtilitiesSecondTry
    {
        public static string GetDifferentConnectionString(string keyname)
        {
            string connection = string.Empty;
            switch (keyname)
            {
                case "xe_josee":
                    connection = ConfigurationManager.ConnectionStrings["xe_josee"].ConnectionString;
                    break;
                case "xe_user_client_details":
                    connection = ConfigurationManager.ConnectionStrings["xe_user_client_details"].ConnectionString;
                    break;
                default:
                    break;
            }
            return connection;
        }
    }
}
