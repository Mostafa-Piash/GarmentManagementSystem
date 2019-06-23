using GMS.Entity;
using GMS.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL
{
    public class OrderListData
    {
        List<OrderList> GetData(SqlCommand cmd)
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<OrderList> orderList = new List<OrderList>();
            using (reader)
            {
                while (reader.Read())
                {
                    OrderList ord = new OrderList();
                    ord.OrderId = reader.GetInt32(0);
                    ord.OrderItemType = reader.GetString(1);
                    ord.OrderQuantity = reader.GetInt32(2);
                    ord.OrderStatus = reader.GetString(3);
                    orderList.Add(ord);
                }
                reader.Close();
            }
            cmd.Connection.Close();
            return orderList;
        }
        public List<OrderList> GetOrderList()
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * from OrderList");
            List<OrderList> orderList = GetData(cmd);
            return orderList;
        }
        public DataTable GetOrderShowList()
        {
            DataTable dt = new DataTable();
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select * From OrderList");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        public DataTable GetEmployeeOrderList(string eDepTyp)
        {
            DataTable dt = new DataTable();
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select * From OrderList Where OrderId='"+eDepTyp+"'");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        public bool InsertOrder(string oT, int oQ, string oS)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[OrderList] ([OrderItemType],[OrderQuantity],[OrderStatus])" + "VALUES (@oTyp,  @oQun, @oSat)");
            SqlParameter p = new SqlParameter("@oTyp", SqlDbType.VarChar, 20); p.Value = oT;
            SqlParameter p1 = new SqlParameter("@oQun", SqlDbType.Int); p1.Value = oQ;
            SqlParameter p2 = new SqlParameter("@oSat", SqlDbType.VarChar, 20); p2.Value = oS;

            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
        public Boolean ChangeOrderStatus(string oId, string oS)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Update [dbo].[OrderList] Set OrderStatus='" + oS + "' where OrderId= '" + oId + "'");

            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
    }
}
