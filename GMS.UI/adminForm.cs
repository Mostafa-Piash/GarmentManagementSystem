using GMS.DAL;
using GMS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS.UI
{
    
    public partial class adminForm : MetroFramework.Forms.MetroForm
    {
        public string admId;
        public string clickId=null;
        public string clickDep=null;
        public adminForm(string a)
        {
            InitializeComponent();
            this.admId = a;
        }

        private void adminForm_Load(object sender, EventArgs e)
        {
            metroComboBox1.Items.Add("Admin");
            metroComboBox1.Items.Add("Manager");
            metroComboBox1.Items.Add("Employee");

            metroComboBox2.Items.Add("Cutting");
            metroComboBox2.Items.Add("Sewing");
            metroComboBox2.Items.Add("Dyeing");
            metroComboBox2.Items.Add("Packing");

            Init();
        }
        

        private void adminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            mainForm mf = new mainForm();
            mf.Show(this);
            this.Hide();
        }
        public void Init()
        {
            AdminData ad = new AdminData();
            metroGrid1.DataSource = ad.GetAdminShowList();
            metroGrid2.DataSource = ad.GetAdminOwnInfo(this.admId);
            metroTextBox1.Clear();
            metroTextBox2.Clear();
            metroTextBox3.Clear();
            metroTextBox4.Clear();
            if (metroComboBox1.Text == "Admin" || metroComboBox1.Text == "")
            {
                metroGrid1.DataSource = ad.GetAdminShowList();
            }
            if (metroComboBox1.Text == "Manager")
            {
                ManagerData md = new ManagerData();
                metroGrid1.DataSource = md.GetManagerShowList();
            }
            if (metroComboBox1.Text == "Employee")
            {
                EmployeeData ed = new EmployeeData();
                metroGrid1.DataSource = ed.GetEmployeeShowList();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            
            if (metroComboBox1.SelectedIndex.ToString() == "0")
            {
                AdminData ad = new AdminData();
                if (ad.InsertAdmin(metroTextBox1.Text, metroTextBox2.Text, metroTextBox3.Text) == true)
                {
                    MessageBox.Show("Admin Added","Success");
                    Init();
                }
                else
                {
                    MessageBox.Show("Please Check All The Info Was Correct", "Error");
                }
            }
            if (metroComboBox1.SelectedIndex.ToString() == "1")
            {
                ManagerData md = new ManagerData();
                if (md.InsertManager(metroTextBox1.Text, metroTextBox2.Text, Convert.ToInt32(metroTextBox4.Text),metroComboBox2.Text,metroTextBox3.Text) == true)
                {
                    MessageBox.Show("Manager Added", "Success");
                    Init();
                }
                else
                {
                    MessageBox.Show("Please Check All The Info Was Correct", "Error");
                }
            }
            if (metroComboBox1.SelectedIndex.ToString() == "2")
            {
                EmployeeData ed = new EmployeeData();
                if (ed.InsertEmployee(metroTextBox1.Text, metroTextBox2.Text, Convert.ToInt32(metroTextBox4.Text), metroComboBox2.Text, metroTextBox3.Text) == true)
                {
                    MessageBox.Show("Employee Added", "Success");
                    Init();
                }
                else
                {
                    MessageBox.Show("Please Check All The Info Was Correct", "Error");
                }

            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            AdminData ad = new AdminData();
            if(ad.ChangePass(this.admId, metroTextBox6.Text, metroTextBox5.Text)==true)
            {
                MessageBox.Show("Password Changed","Success");
                Init();
            }
            else
            {
                MessageBox.Show("Sorry there is a problem.Please check again", "Error");
            }
        }

        private void metroGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (metroComboBox1.Text == "Manager")
            {
                var row = this.metroGrid1.Rows[e.RowIndex];
                this.clickId = row.Cells["ManagerID"].Value.ToString();
                metroTextBox1.Text = row.Cells["ManagerName"].Value.ToString();
                metroTextBox4.Text = row.Cells["ManagerSalary"].Value.ToString();
                this.clickDep = row.Cells["ManagerDepartmentName"].Value.ToString();
                metroTextBox3.Text = row.Cells["ManagerEmail"].Value.ToString();
            }
            if (metroComboBox1.Text == "Employee")
            {
                var row = this.metroGrid1.Rows[e.RowIndex];
                this.clickId = row.Cells["EmployeeID"].Value.ToString();
                metroTextBox1.Text = row.Cells["EmployeeName"].Value.ToString();
                metroTextBox4.Text = row.Cells["EmployeeSalary"].Value.ToString();
                this.clickDep = row.Cells["EmployeeDepartmentName"].Value.ToString();
                metroTextBox3.Text = row.Cells["EmployeeEmail"].Value.ToString();
            }
            if (metroComboBox1.Text == "Admin" || metroComboBox1.Text == "")
            {
                var row = this.metroGrid1.Rows[e.RowIndex];
                this.clickId = row.Cells["AdminID"].Value.ToString();
                metroTextBox1.Text = row.Cells["AdminName"].Value.ToString();
                metroTextBox3.Text = row.Cells["AdminEmail"].Value.ToString();
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedIndex.ToString() == "1")
            {
                ManagerData md = new ManagerData();
                if (this.clickId != null)
                {
                    if (md.DeleteManager(this.clickId) == true)
                    {
                        MessageBox.Show("Manager Removed", "Success");
                        Init();
                    }
                    else
                    {
                        MessageBox.Show("Please Click On A Manageer Data", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Please Click On A Manager Data", "Error");
                    Init();
                }
            }
            if (metroComboBox1.SelectedIndex.ToString() == "2")
            {
                EmployeeData ed = new EmployeeData();
                if (this.clickId != null)
                {
                    if (ed.DeleteEmployee(this.clickId) == true)
                    {
                        MessageBox.Show("Employee Removed", "Success");
                        Init();
                    }
                    else
                    {
                        MessageBox.Show("Please Click On A Employee Data", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Please Click On A Employee Data", "Error");
                    Init();
                }
            }
            else
            {
                AdminData ad = new AdminData();
                if (this.clickId != null)
                {
                    if (ad.DeleteAdmin(this.clickId) == true)
                    {
                        MessageBox.Show("Admin Removed", "Success");
                        Init();
                    }
                    else
                    {
                        MessageBox.Show("Please Click On A Admin Data", "Error");
                        Init();
                    }
                }
                else
                {
                    MessageBox.Show("Please Click On A Admin Data", "Error");
                    Init();
                }
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "Admin" || metroComboBox1.Text == "")
            {
                AdminData ad = new AdminData();
                if(ad.UpdateAdmin(this.clickId,metroTextBox1.Text,metroTextBox3.Text)==true)
                {
                    MessageBox.Show("Admin Updated", "Success");
                    Init();
                }
                else
                {
                    MessageBox.Show("Please check again.","Error");
                    Init();
                }
            }
            if (metroComboBox1.Text == "Manager")
            {
                if (metroComboBox2.Text != "")
                {
                    ManagerData md = new ManagerData();
                    if (md.UpdateManager(this.clickId, metroTextBox1.Text, Convert.ToInt32(metroTextBox4.Text), metroComboBox2.Text, metroTextBox3.Text) == true)
                    {
                        MessageBox.Show("Manager Updated", "Success");
                        Init();
                    }
                    else
                    {
                        MessageBox.Show("Please check again.", "Error");
                        Init();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Department", "Error");
                    Init();
                }
            }
            if (metroComboBox1.Text == "Employee")
            {
                if (metroComboBox2.Text != "")
                {
                    EmployeeData ed = new EmployeeData();
                    if (ed.UpdateEmployee(this.clickId, metroTextBox1.Text, Convert.ToInt32(metroTextBox4.Text), metroComboBox2.Text, metroTextBox3.Text) == true)
                    {
                        MessageBox.Show("Employee Updated", "Success");
                        Init();
                    }
                    else
                    {
                        MessageBox.Show("Please check again.", "Error");
                        Init();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Department", "Error");
                    Init();
                }

            }
            
        }
    }
}
