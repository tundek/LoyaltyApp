using Loyalty.BLL;
using Loyalty.DAL;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loyalty.UI
{
    public partial class LoyaltyManagement : Form
    {
        public LoyaltyManagement()
        {
            InitializeComponent();
        }

        public static string myconnstring = ConfigurationManager.ConnectionStrings["customerLoyaltyConnectionString"].ConnectionString;

        string operation;
        CustomerBLL cb = new CustomerBLL();
        CustomerDAL cd = new CustomerDAL();

        SqlConnection con = new SqlConnection(myconnstring);
        
        private void label3_Click(object sender, EventArgs e)
        {

        }



      /*
        private void button1_Click(object sender, EventArgs e)
        {
            operation = "Points Added";
            if (txtAddPoints.Text == "")
            {
                MessageBox.Show("Enter total Bill Amount");
            }
            else
            {
                double pointAmount;
                pointAmount = double.Parse(txtAddPoints.Text);

                double bonus = 0.05 * pointAmount;



                string sql = "UPDATE customer SET points = points + '" + bonus + "' WHERE customerID = '" + txtSearch.Text + "' ";

                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@id", cb.cust_id);

                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        MessageBox.Show("Points successfuly added");
                        txtAddPoints.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Sorry!! Something went wrong");

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

                Display();
            }
            
        }

        */

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtID.Text = "";
            txtCID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
        }

        public void Display()
        {
            DataTable dt = cd.Select();
            dataGridView1.DataSource = dt;
        }
        private void LoyaltyManagement_Load(object sender, EventArgs e)
        {

            Display();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get the index of a particular row

            int rowIndex = e.RowIndex;
            txtID.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            txtPhone.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            txtCID.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            lblPointValue.Text = dataGridView1.Rows[rowIndex].Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get values from customer UI
            try
            {
                cb.firstname = txtName.Text;
                cb.phone = txtPhone.Text;
                cb.CustomerID = txtCID.Text;
                cb.Customer_Type = comboBox1.Text;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            // Updating data in database

            bool success = cd.Update(cb);

            //If Data is updated successfully
            if (success == true)
            {
                MessageBox.Show("Data Updated Successfully..");
                Clear();
                
            }
            else
            {
                MessageBox.Show("ERROR!! Cannot Update data");
               // Clear();
            }
            // Refresh the GridView
            Display();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                cb.cust_id = Convert.ToInt16(txtID.Text);
                cb.firstname = txtName.Text;
                cb.phone = txtPhone.Text;
                cb.CustomerID = txtCID.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (MessageBox.Show("Sure you want to Delete Customer Data??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                 bool success = cd.Delete(cb);

                 if(success == true)
                 {
                      MessageBox.Show("Deleted Successfully");
                      Clear();
                 }   
            }
            else
            {
                MessageBox.Show("ERROR deleting Data");
            }
            // Refreshes the GridView
            Display();
        }

    
        /*
        private void btnRedeemPoint_Click(object sender, EventArgs e)
        {
            if (txtAddPoints.Text == "")
            {
                MessageBox.Show("Enter total Bill Amount");
            }
            else
            {
               
                double pointAmount = double.Parse(txtAddPoints.Text);

                string sql = "UPDATE customer SET points = points - '" + pointAmount + "' WHERE customerID = '" + txtSearch.Text + "' ";

                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@id", cb.cust_id);

                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        MessageBox.Show("HURRAY!! You have successfully redeemed your points for GIFT");
                        txtAddPoints.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Sorry!! Something went wrong");

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

                Display();
            }
        }
        */

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Customers WHERE customerID = '"+txtSearch.Text+"' ";

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lblPointValue.Text = dr[6].ToString();
                    
                }
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string sql = "SELECT * FROM Customers WHERE customerID LIKE '"+txtSearch.Text+"%' or phone LIKE '"+txtSearch.Text+"%' ";

            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql,con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
