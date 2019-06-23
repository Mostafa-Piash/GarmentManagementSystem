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
    public class ManagerData
    {
        List<Manager> GetData(SqlCommand cmd)
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Manager> managerList = new List<Manager>();
            using (reader)
            {
                while (reader.Read())
                {
                    Manager man = new Manager();
                    man.ManagerId = reader.GetInt32(0);
                    man.ManagerName = reader.GetString(1);
                    man.ManagerPassword = reader.GetString(2);
                    man.ManagerSalary = reader.GetInt32(3);
                    man.ManagerDepartmentName = reader.GetString(4);
                    man.ManagerEmail = reader.GetString(5);
                    managerList.Add(man);
                }
                reader.Close();
            }
            cmd.Connection.Close();
            return managerList;
        }
        public List<Manager> GetManagerList()
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * from Manager");
            List<Manager> managerList = GetData(cmd);
            return managerList;
        }
        public DataTable GetManagerShowList()
        {
            DataTable dt = new DataTable();
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select ManagerId,ManagerName,ManagerSalary,ManagerDepartmentName,ManagerEmail From Manager");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        public DataTable GetManagerOwnInfo(string a)
        {
            DataTable dt = new DataTable();
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select ManagerId,ManagerName,ManagerSalary,ManagerDepartmentName,ManagerEmail From Manager where ManagerId ='" + a + "'");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }

        public Boolean checkManager(string mId, string mPass)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select * From Manager where ManagerId='" + mId + "'and ManagerPassword = '" + mPass + "'");
            List<Manager> managerList = GetData(cmd);
            if (managerList != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertManager(string mN, string mP, int mS, string mD, string mE)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[Manager] ([ManagerName],[ManagerPassword],[ManagerSalary],[ManagerDepartmentName],[ManagerEmail])" + "VALUES (@mName,  @mPass, @mSal, @mDep, @mEmail)");
            SqlParameter p = new SqlParameter("@mName", SqlDbType.VarChar, 20); p.Value = mN;
            SqlParameter p1 = new SqlParameter("@mPass", SqlDbType.VarChar, 20); p1.Value = mP;
            SqlParameter p2 = new SqlParameter("@mSal", SqlDbType.Int); p2.Value = mS;
            SqlParameter p3 = new SqlParameter("@mDep", SqlDbType.VarChar, 20); p3.Value = mD;
            SqlParameter p4 = new SqlParameter("@mEmail", SqlDbType.VarChar, 20); p4.Value = mE;

            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
        public Boolean ChangePass(string mid,string op, string np)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Update [dbo].[Manager] Set ManagerPassword='" + np + "' where ManagerId= '" + mid + "' and ManagerPassword'"+op+"'");

            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
        public Boolean DeleteManager(string mid)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Delete From [dbo].[Manager] where ManagerId= '" + mid + "'");

            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
        public Boolean UpdateManager(string mI,string mN,int mS,string mD,string mE)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Update [dbo].[Manager] Set ManagerName='" + mN + "',ManagerSalary='"+mS+"',ManagerDepartmentName='"+mD+"',ManagerEmail='"+mE+"' where ManagerId= '" + mI + "'");

            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
    }
}
