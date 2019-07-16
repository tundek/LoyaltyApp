using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Loyalty.DAL;
using Loyalty.BLL;

namespace Loyalty.UI
{
    public partial class AddPoints : Form
    {
       // PointsDAL pd = new PointsDAL();
       // PointsBLL pb = new PointsBLL();
        ReceiptBLL rb = new ReceiptBLL();
        ReceiptDAL rd = new ReceiptDAL();
        CustomerBLL cb = new CustomerBLL();
        CustomerDAL cd = new CustomerDAL();
        //public static string myconnstring = ConfigurationManager.ConnectionStrings["customerLoyaltyConnectionString"].ConnectionString;
        //SqlConnection con = new SqlConnection(myconnstring);
        public AddPoints()
        {
            InitializeComponent();
        }

        private void AddPoints_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string operation = "Points Added";


            if (txtBillAmount.Text == string.Empty || txtCustomerID.Text == string.Empty || txtReceiptNo.Text == string.Empty)
            {
                MessageBox.Show("ERROR!! One or more Fields is empty");
            }
            else
            {
                if (rd.DuplicateBill(txtReceiptNo.Text.Trim()) == true)
                {
                    MessageBox.Show("SORRY!! This Bill has already been used!");
                }
                else
                {
                    rb.ReceiptNo = txtReceiptNo.Text.Trim();
                    rb.BillAmount = double.Parse(txtBillAmount.Text.Trim());
                    cb.Customer_Type = cd.CustomerType(txtCustomerID.Text.Trim(), cb.Customer_Type).ToString();
                    switch (cb.Customer_Type)
                    {
                        case "Retailer":
                            rb.PointsAdded = (double.Parse(txtBillAmount.Text.Trim()) * 0.05);
                            break;
                        case "Wholesaler":
                            rb.PointsAdded = (double.Parse(txtBillAmount.Text.Trim()) * 0.01);
                            break;
                        default:
                            rb.PointsAdded = (double.Parse(txtBillAmount.Text.Trim()) * 0.05);
                            break;
                    }
                    rb.Operation = operation;
                    rb.PointsRedeemed = 0.00;
                    rb.CustomerID = txtCustomerID.Text.Trim();
                    rb.date = DateTime.Now.ToString();

                    if (cd.DuplicateCustomer(cb.CustomerID) == true)
                    {
                        MessageBox.Show("This card has NOT been registered to any Customer. Kindly register it");
                    }

                    else
                    {
                        bool success = rd.Insert(rb);

                        if (success == true)
                        {
                            MessageBox.Show("Points Successfully Added");
                            
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed adding points");
                            Clear();
                        }
                    }

                }
               
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtReceiptNo.Text = string.Empty;
            txtBillAmount.Text = string.Empty;
            txtCustomerID.Text = string.Empty;
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            if(cd.DuplicateCustomer(txtCustomerID.Text.Trim()) == true)
            {
                lblPointsAvailable.Text = cd.CustomerIDandPointsAvailable(txtCustomerID.Text.Trim(), cb.PointsAvailable.ToString());
            }
            else
            {
                MessageBox.Show("SORRY, this customer has not been Registered");
            }
            
        }
    }
}
