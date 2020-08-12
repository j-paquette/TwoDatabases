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
        public static string GetDifferentConnectionStringByName(string keyname)
        {
            //string connection = string.Empty;

            var connection = ConfigurationManager.ConnectionStrings[keyname]?.ConnectionString;
            return connection;
        }
    }
}
