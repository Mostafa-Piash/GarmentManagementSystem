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
    public partial class mainForm : MetroFramework.Forms.MetroForm
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            metroComboBox1.Items.Add("Admin");
            metroComboBox1.Items.Add("Manager");
            metroComboBox1.Items.Add("Employee");
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "Admin")
            {
                AdminData ad = new AdminData();

                if (ad.checkAdmin(metroTextBox1.Text,metroTextBox2.Text)== true)
                {

                    adminForm af = new adminForm(metroTextBox1.Text);
                    af.Show(this);
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Check ID and Password again.", "Error");
                }

            }
            if (metroComboBox1.Text == "Manager")
            {
                ManagerData md = new ManagerData();

                if (md.checkManager(metroTextBox1.Text, metroTextBox2.Text) == true)
                {

                    managerForm mf = new managerForm(metroTextBox1.Text);
                    mf.Show(this);
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Check ID and Password again.", "Error");
                }

            }
            if (metroComboBox1.Text == "Employee")
            {
                EmployeeData ed = new EmployeeData();

                if (ed.checkEmployee(metroTextBox1.Text, metroTextBox2.Text) == true)
                {
                    employeeForm ef = new employeeForm(metroTextBox1.Text);
                    ef.Show(this);
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Check ID and Password again.", "Error");
                }

            }
            if(metroComboBox1.Text == "")
            {
                MessageBox.Show("Please Select An User Type", "Error");
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }

