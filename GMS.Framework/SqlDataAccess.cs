using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Framework
{
    public class SqlDataAccess
    {
        const string ConnectiosnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jesan\Desktop\GarmentManagementSystem\DB\GMSdb.mdf;Integrated Security=True;Connect Timeout=30";
        public SqlCommand GetCommand(String query)
        {
            var connection = new SqlConnection(ConnectiosnString);
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = connection;
            return cmd;
        }
    }
}
