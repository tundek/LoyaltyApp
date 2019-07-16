using Loyalty.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loyalty.DAL
{
    class CustomerDAL
    {
        public static string myconnstring = ConfigurationManager.ConnectionStrings["customerLoyaltyConnectionString"].ConnectionString;


        #region DuplicateMobile
        public bool DuplicateRFID(CustomerBLL c)
        {
            bool isSuccess = false;

            SqlConnection con = new SqlConnection(myconnstring);
           // DataTable dt = new DataTable();
            
            try
            {
                string sql = "SELECT * FROM Customers WHERE customerID = @customerID";
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@customerID",c.CustomerID);
 
                con.Open();

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    isSuccess = false;
                }
                else
                {
                    isSuccess = true;
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
            return isSuccess;
        }

        #endregion

        #region Select Data from database

        public DataTable Select()
        {
            SqlConnection con = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql ="SELECT * FROM Customers";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        #endregion

        #region Insert Data in database

        public bool Insert(CustomerBLL c)
        {
            bool isSuccess = false;
            //string operation = "Customer Added";
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                
                string sql = "INSERT into Customers(firstname,phone,CustomerID,Customer_Type,joinDate,PointsAvailable) VALUES (@first_name,@phone,@CustomerID,@Customer_Type,@joinDate,@PointsAvailable)";
                //string sql1 = "INSERT into Points(points,operations) VALUES(0,'"+operation+"')";
                SqlCommand cmd = new SqlCommand(sql,con);
               // SqlCommand cmd1 = new SqlCommand(sql1, con);
                con.Open();
                cmd.Parameters.AddWithValue("@first_name", c.firstname);
                cmd.Parameters.AddWithValue("@phone", c.phone);
                cmd.Parameters.AddWithValue("@customerID", c.CustomerID);
                cmd.Parameters.AddWithValue("@Customer_Type", c.Customer_Type);
                cmd.Parameters.AddWithValue("@joinDate",c.joinDate);
                cmd.Parameters.AddWithValue("@PointsAvailable", c.PointsAvailable);
                


                int rows = cmd.ExecuteNonQuery();

                //cmd1.ExecuteNonQuery();

                if (rows == 0)
                {
                    isSuccess = false;
                }
                else
                {
                    isSuccess = true;
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
            return isSuccess;
        }

        #endregion

        #region Update Data in Database

        public bool Update(CustomerBLL c)
        {
            bool isSuccess = false;

            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "UPDATE Customers SET firstname = @firstname, phone = @phone, CustomerID =@CustomerID, Customer_Type=@Customer_Type WHERE CustomerID = @CustomerID ";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@firstname", c.firstname);
                cmd.Parameters.AddWithValue("@phone", c.phone);
                cmd.Parameters.AddWithValue("@CustomerID",c.CustomerID);            
                cmd.Parameters.AddWithValue("@Customer_Type", c.Customer_Type);
                //cmd.Parameters.AddWithValue("@points", c.points);
                
                con.Open();
                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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


            return isSuccess;
        }

        #endregion

        #region Delete Data from Database

        public bool Delete(CustomerBLL c)
        {

            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM Customers WHERE cust_id=@id";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", c.cust_id);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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

            return isSuccess;
        }

        #endregion

        #region DuplicateCustomer

        public bool DuplicateCustomer(string customerID)
        {
            SqlConnection con = new SqlConnection(myconnstring);

            bool isSuccess = false;
            
            string sql = "SELECT * FROM Customers WHERE customerID='"+customerID+"' ";
            
            try
            {
                CustomerBLL cb = new CustomerBLL();
                con.Open();
                SqlCommand cmd = new SqlCommand(sql,con);
                //cmd.Parameters.AddWithValue("@customerID",);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = ds.Tables[0].Rows.Count;

                if (i > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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

            return isSuccess;
        }
        #endregion

        #region CustomerType

        public string CustomerType(string customerID,string value)
        {
            
            CustomerBLL cb = new CustomerBLL();
            SqlConnection con = new SqlConnection(myconnstring);
            string sql = "SELECT * from Customers WHERE CustomerID= '"+customerID+"' ";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    value = dr[4].ToString();

                }
                
               /* con.Open();

                SqlDataAdapter da = new SqlDataAdapter(sql,con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cb.Customer_Type = dt.Rows[3]["Customer_Type"].ToString();
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return value;
        }

        #endregion



        #region RetrieveCustomerID

        public CustomerBLL CustomerIdentity(PointsBLL id)
        {
            CustomerBLL cb = new CustomerBLL();
            SqlConnection con = new SqlConnection(myconnstring);
            string sql = "SELECT cust_id from Customers WHERE id='" + id + "' ";

            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cb.cust_id = int.Parse(dt.Rows[0]["cust_id"].ToString());
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

            return cb;
        }


        #endregion

        #region SELECT CUSTOMER AND AVAILABLE POINTS

        public string CustomerIDandPointsAvailable(string customerID,string value)
        {
            CustomerBLL cb = new CustomerBLL();
            SqlConnection con = new SqlConnection(myconnstring);
            string sql = "SELECT * from Customers WHERE CustomerID= '" + customerID + "' ";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    value = dr[6].ToString();
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
            return value;
            
        }

        #endregion
    }
}
