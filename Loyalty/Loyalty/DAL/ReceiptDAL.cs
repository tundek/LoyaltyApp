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
    class ReceiptDAL
    {

        static string myconnstring = ConfigurationManager.ConnectionStrings["customerLoyaltyConnectionString"].ConnectionString;

        #region SELECT POINT DETAILS

        public DataTable Select()
        {
            SqlConnection con = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "SELECT * FROM Receipts";
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

        #region INSERT POINTS

        public bool Insert(ReceiptBLL rb)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            bool isSuccess = false;

            try
            {

                con.Open();
                string sql = "INSERT INTO Receipts(ReceiptNo,BillAmount,PointsAdded,Operation,PointsRedeemed,CustomerID,date) VALUES(@ReceiptNo,@BillAmount,@PointsAdded,@Operation,@PointsRedeemed,@CustomerID,@date) ";
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@ReceiptNo",rb.ReceiptNo);
                cmd.Parameters.AddWithValue("@BillAmount", rb.BillAmount);
                cmd.Parameters.AddWithValue("@PointsAdded", rb.PointsAdded);
                cmd.Parameters.AddWithValue("@Operation", rb.Operation);
                cmd.Parameters.AddWithValue("@PointsRedeemed", rb.PointsRedeemed);
                cmd.Parameters.AddWithValue("@CustomerID", rb.CustomerID);
                cmd.Parameters.AddWithValue("@date", rb.date);

                int row = cmd.ExecuteNonQuery();

                string sql1 = "UPDATE Customers SET PointsAvailable=PointsAvailable + @PointsAdded WHERE CustomerID=@CustomerID";
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd1.Parameters.AddWithValue("@PointsAdded",rb.PointsAdded);
                cmd1.Parameters.AddWithValue("@CustomerID", rb.CustomerID);
                cmd1.ExecuteNonQuery();

                if (row > 0)
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

        #region Duplicate Bill

        public bool DuplicateBill(string receiptno)
        {
            SqlConnection con = new SqlConnection(myconnstring);

            bool isSuccess = false;

            string sql = "SELECT * FROM Receipts WHERE ReceiptNo='" + receiptno + "' ";

            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
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


        #region UPDATE POINTS


        public bool UpdatePoint(ReceiptBLL rb)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            bool isSuccess = false;

            try
            {

                con.Open();
                string sql = "UPDATE Receipts SET ReceiptNo=@ReceiptNo ,BillAmount=@BillAmount, PointsAdded=@PointsAdded, Operation=@Operation, PointsRedeemed=@PointsRedeemed, CustomerID=@CustomerID,date = @date WHERE CustomerID=@CustomerID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ReceiptNo", rb.ReceiptNo);
                cmd.Parameters.AddWithValue("@BillAmount", rb.BillAmount);
                cmd.Parameters.AddWithValue("@PointsAdded", rb.PointsAdded);
                cmd.Parameters.AddWithValue("@Operation", rb.Operation);
                cmd.Parameters.AddWithValue("@PointsRedeemed", rb.PointsRedeemed);
                cmd.Parameters.AddWithValue("@CustomerID", rb.CustomerID);
                cmd.Parameters.AddWithValue("@date", rb.date);

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
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



        #region REDEEM Points

        public bool RedeemPoints(ReceiptBLL rb)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            bool isSuccess = false;

            try
            {

                con.Open();

                string sql = "INSERT INTO Receipts(ReceiptNo,BillAmount,PointsAdded,Operation,PointsRedeemed,CustomerID,date) VALUES(@ReceiptNo,@BillAmount,@PointsAdded,@Operation,@PointsRedeemed,@CustomerID,@date) ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ReceiptNo", rb.ReceiptNo);
                cmd.Parameters.AddWithValue("@BillAmount", rb.BillAmount);
                cmd.Parameters.AddWithValue("@PointsAdded", rb.PointsAdded);
                cmd.Parameters.AddWithValue("@Operation", rb.Operation);
                cmd.Parameters.AddWithValue("@PointsRedeemed", rb.PointsRedeemed);
                cmd.Parameters.AddWithValue("@CustomerID", rb.CustomerID);
                cmd.Parameters.AddWithValue("@date", rb.date);

                int row = cmd.ExecuteNonQuery();


                string sql1 = "UPDATE Customers SET PointsAvailable=PointsAvailable - @PointsRedeemed WHERE CustomerID=@CustomerID";
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd1.Parameters.AddWithValue("@PointsRedeemed", rb.PointsRedeemed);
                cmd1.Parameters.AddWithValue("@CustomerID", rb.CustomerID);
                
                cmd1.ExecuteNonQuery();
                if (row > 0)
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

    }
}
