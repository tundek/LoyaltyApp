using Loyalty.BLL;
using Loyalty.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Loyalty
{
    public partial class AddNewCustomer : Form
    {
        public AddNewCustomer()
        {
            InitializeComponent();
        }

        public static string myconnstring = ConfigurationManager.ConnectionStrings["customerLoyaltyConnectionString"].ConnectionString;

        CustomerBLL cb = new CustomerBLL();
        CustomerDAL cd = new CustomerDAL();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewCustomer_Load(object sender, EventArgs e)
        {

        }

        public void Clear()
        {
            txtCustomerID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            comboBox1.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            
            if (txtCustomerID.Text == "" || txtName.Text == "" || txtPhone.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Error!!, Customer Details cannot be BLANK");
                //return;
            }
            
            else
            {
               
                cb.firstname = txtName.Text;
                cb.phone = txtPhone.Text;
                cb.CustomerID = txtCustomerID.Text;
                cb.Customer_Type = comboBox1.Text;
                cb.joinDate = DateTime.Now.ToString();
                cb.PointsAvailable = 0.00;
                if (cd.DuplicateCustomer(cb.CustomerID) == true)
                {
                    MessageBox.Show("SORRY!! A customer has been registered to this card..");
                    return;
                }
                //cb.points = 0;

                bool success = cd.Insert(cb);

                if (success == true)
                {
                    MessageBox.Show("Customer has been Successfully Saved");
                    Clear();
                }
                else
                {
                    MessageBox.Show("Failed to add Customer");
                }
         
            }
        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
