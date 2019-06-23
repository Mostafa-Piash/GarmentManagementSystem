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
    public class EmployeeData
    {
        List<Employee> GetData(SqlCommand cmd)
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Employee> employeeList = new List<Employee>();
            using (reader)
            {
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.EmployeeId = reader.GetInt32(0);
                    emp.EmployeeName = reader.GetString(1);
                    emp.EmployeePassword = reader.GetString(2);
                    emp.EmployeeSalary = reader.GetInt32(3);
                    emp.EmployeeDepartmentName = reader.GetString(4);
                    emp.EmployeeEmail = reader.GetString(5);
                    employeeList.Add(emp);
                }
                reader.Close();
            }
            cmd.Connection.Close();
            return employeeList;
        }
        public List<Employee> GetEmployeeList()
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * from Employee");
            List<Employee> employeeList = GetData(cmd);
            return employeeList;
        }
        public DataTable GetEmployeeShowList()
        {
            DataTable dt = new DataTable();
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select EmployeeId,EmployeeName,EmployeeSalary,EmployeeDepartmentName,EmployeeEmail From Employee");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        public DataTable GetEmployeeOwnInfo(string a)
        {
            DataTable dt = new DataTable();
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select EmployeeId,EmployeeName,EmployeeSalary,EmployeeDepartmentName,EmployeeEmail From Employee where EmployeeId ='" + a + "'");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        //public string GetEmployeeDepartment(String a)
        //{
        //    string
        //}
        public Boolean checkEmployee(string eId, string ePass)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select * From Employee where EmployeeId='" + eId + "'and EmployeePassword = '" + ePass + "'");
            List<Employee> employeeList = GetData(cmd);
            if (employeeList != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertEmployee(string eN, string eP, int eS, string eD, string eE)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[Employee] ([EmployeeName],[EmployeePassword],[EmployeeSalary],[EmployeeDepartmentName],[EmployeeEmail])" + "VALUES (@eName,  @ePass, @eSal, @eDep, @eEmail)");
            SqlParameter p = new SqlParameter("@eName", SqlDbType.VarChar, 20); p.Value = eN;
            SqlParameter p1 = new SqlParameter("@ePass", SqlDbType.VarChar, 20); p1.Value = eP;
            SqlParameter p2 = new SqlParameter("@eSal", SqlDbType.Int); p2.Value = eS;
            SqlParameter p3 = new SqlParameter("@eDep", SqlDbType.VarChar, 20); p3.Value = eD;
            SqlParameter p4 = new SqlParameter("@eEmail", SqlDbType.VarChar, 20); p4.Value = eE;

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
        public Boolean ChangePass(string eId, string op,string np)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Update [dbo].[Employee] Set EmployeePassword='" + np + "' where EmployeeId= '" + eId + "' and EmployeePassword='"+op+"'");

            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
        public Boolean DeleteEmployee(string eId)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Delete From [dbo].[Employee] where EmployeeId= '" + eId + "'");

            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
        public DataTable GetEmployeeOrder(string eDep)
        {
            DataTable dt = new DataTable();
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Select * From OrderList where OrderStatus ='" + eDep + "'");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        public Boolean UpdateEmployee(string eI, string eN, int eS, string eD, string eE)
        {
            SqlDataAccess da = new SqlDataAccess();
            SqlCommand cmd = da.GetCommand("Update [dbo].[Employee] Set EmployeeName='" + eN + "',EmployeeSalary='" + eS + "',EmployeeDepartmentName='" + eD + "',EmployeeEmail='" + eE + "' where EmployeeId= '" + eI + "'");

            cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return val > 0;
        }
    }
}
