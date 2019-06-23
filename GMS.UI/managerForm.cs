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
    public partial class managerForm : MetroFramework.Forms.MetroForm
    {
        public string manId;
        public string ordId=null;
        public string ordItem=null;
        public string ordDep=null;
        public managerForm(string a)
        {
            InitializeComponent();
            this.manId = a;
        }

        private void managerForm_Load(object sender, EventArgs e)
        {
            metroComboBox1.Items.Add("Shirt(F)");
            metroComboBox1.Items.Add("Shirt(H)");
            metroComboBox1.Items.Add("T-Shirt");
            metroComboBox1.Items.Add("Pant(G)");
            metroComboBox1.Items.Add("Pant(J)");
            metroComboBox1.Items.Add("Pant(3)");

            metroComboBox2.Items.Add("Cutting");
            metroComboBox2.Items.Add("Sewing");
            metroComboBox2.Items.Add("Dyeing");
            metroComboBox2.Items.Add("Packing");
            Init2();
        }

        private void managerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            mainForm mf = new mainForm();
            mf.Show(this);
            this.Hide();
        }
        public void Init2()
        {
            
            metroTextBox1.Clear();
            EmployeeData ed = new EmployeeData();
            metroGrid1.DataSource = ed.GetEmployeeShowList();
            ManagerData md = new ManagerData();
            metroGrid2.DataSource = md.GetManagerShowList();
            metroGrid4.DataSource = md.GetManagerOwnInfo(this.manId);
            OrderListData od = new OrderListData();
            metroGrid3.DataSource = od.GetOrderShowList();

        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            OrderListData old = new OrderListData();
            old.InsertOrder(metroComboBox1.Text,Convert.ToInt32(metroTextBox1.Text),"Redieved");
            MessageBox.Show("Order Added","Success");
            Init2();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            ManagerData md = new ManagerData();
            if(md.ChangePass(this.manId, metroTextBox2.Text, metroTextBox5.Text)==true)
            {
                MessageBox.Show("Password Changed","Success");
                Init2();
            }
            else
            {
                MessageBox.Show("Sorry there is a problem.Please check again", "Error");
            }

        }

        private void metroGrid3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = this.metroGrid3.Rows[e.RowIndex];
            this.ordId = row.Cells["OrderID"].Value.ToString();
            metroTextBox1.Text = row.Cells["OrderQuantity"].Value.ToString();
            this.ordItem = row.Cells["OrderItemType"].Value.ToString();
            this.ordDep = row.Cells["OrderStatus"].Value.ToString();

        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if(this.ordId!=null)
            {
                if(metroComboBox2.Text!="")
                {
                    OrderListData oLd = new OrderListData();
                    if(oLd.ChangeOrderStatus(this.ordId,metroComboBox2.Text)==true)
                    {
                        MessageBox.Show("Order Assigned","Success");
                        Init2();
                    }
                    else
                    {
                        MessageBox.Show("Please Check Again", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Please Select A Department", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please Click On An Order Data", "Error");
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (this.ordId != null)
            {
                    OrderListData oLd = new OrderListData();
                    if (oLd.ChangeOrderStatus(this.ordId, "Cancelled") == true)
                    {
                        MessageBox.Show("Order Cancelled", "Success");
                        Init2();
                    }
                    else
                    {
                        MessageBox.Show("Please Check Again", "Error");
                    }
                
            }
            else
            {
                MessageBox.Show("Please Click On An Order Data", "Error");
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (this.ordId != null)
            {
                OrderListData oLd = new OrderListData();
                if (oLd.ChangeOrderStatus(this.ordId, "Delivered") == true)
                {
                    MessageBox.Show("Order Delivered", "Success");
                    Init2();
                }
                else
                {
                    MessageBox.Show("Please Check Again", "Error");
                }

            }
            else
            {
                MessageBox.Show("Please Click On An Order Data", "Error");
            }
        }
    }
}
