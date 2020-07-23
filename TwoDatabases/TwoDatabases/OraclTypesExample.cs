using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data.OleDb;


namespace TwoDatabases
{
    public class OraclTypesExample
    {
        public void Setup(string connectionString)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            try
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE OracleTypesTable (MyVarchar2 varchar2(3000),MyNumber number(28,4) PRIMARY KEY,MyDate date, MyRaw raw(255))";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO OracleTypesTable VALUES ('test', 2, to_date('2000-01-11 12:54:01','yyyy-mm-dd hh24:mi:ss'), '0001020304')";
                command.ExecuteNonQuery();
                command.CommandText = "SELECT * FROM OracleTypesTable";
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
