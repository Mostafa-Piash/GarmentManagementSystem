using GMS.DAL;
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
    public partial class employeeForm : MetroFramework.Forms.MetroForm
    {
        public string empId;
        public string empDep;
        public employeeForm(string a)
        {
            InitializeComponent();
            this.empId = a;
        }

        private void employeeForm_Load(object sender, EventArgs e)
        {
            Init3();
            
        }
        

        private void employeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            mainForm mf = new mainForm();
            mf.Show(this);
            this.Hide();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            EmployeeData ed = new EmployeeData();
            if (ed.ChangePass(this.empId, metroTextBox1.Text, metroTextBox5.Text)==true)
            {
                MessageBox.Show("Password Changed","Success");
                Init3();
            }
            else
            {
                MessageBox.Show("Please Check Again", "Error");
            }
        }

        private void Init3()
        {
            metroTextBox5.Clear();
            EmployeeData ed = new EmployeeData();
            metroGrid2.DataSource = ed.GetEmployeeOwnInfo(this.empId);
            metroGrid1.DataSource = ed.GetEmployeeOrder(this.empDep);
        }

        private void metroGrid2_MouseMove(object sender, MouseEventArgs e)
        {
            var row = this.metroGrid2.Rows[0];
            this.empDep = row.Cells["EmployeeDepartmentName"].Value.ToString();
            Init3();
        }
    }
}
