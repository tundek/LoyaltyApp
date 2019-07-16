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

namespace Loyalty.UI
{
    public partial class RedeemPoints : Form
    {
        CustomerBLL cb = new CustomerBLL();
        CustomerDAL cd = new CustomerDAL();
        ReceiptBLL rb = new ReceiptBLL();
        ReceiptDAL rd = new ReceiptDAL();
        public RedeemPoints()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RedeemPoints_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            string operation = "Points Redeemed";
            rb.ReceiptNo = "NONE";
            rb.BillAmount = 0.00;
            rb.PointsAdded = 0.00;
            rb.Operation = operation;
            rb.PointsRedeemed = double.Parse(txtRedeemAmount.Text.Trim());
            rb.CustomerID = txtCustomerID.Text.Trim();
            rb.date = DateTime.Now.ToString();

            string balance = cd.CustomerIDandPointsAvailable(txtCustomerID.Text.Trim(), cb.PointsAvailable.ToString());
            if (rb.PointsRedeemed > double.Parse(balance))
            {
                MessageBox.Show("ERROR!! Insufficient Points Balance");
                return;
            }
                
            else
            {
                bool success = rd.RedeemPoints(rb);

                if (success == true)
                {

                    MessageBox.Show("Point Successfully Redeemed");
                }
                else
                {
                    MessageBox.Show("Failed to Redeem Points. Something went Wrong");

                }    
            }


        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {

            if (txtCustomerID.Text == string.Empty && txtRedeemAmount.Text == string.Empty)
            {
                MessageBox.Show("ERROR!! One or more Fields is empty");
            }
            else
            {
                if (cd.DuplicateCustomer(txtCustomerID.Text.Trim()) == true)
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
}
