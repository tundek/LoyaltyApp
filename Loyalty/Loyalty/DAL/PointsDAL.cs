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
    class PointsDAL
    {
        public static string myconnstring = ConfigurationManager.ConnectionStrings["customerLoyaltyConnectionString"].ConnectionString;

        #region Select from Point table
        public DataTable Select()
        {
            SqlConnection con = new SqlConnection(myconnstring);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM Points";
                
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
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

        #region Insert Into the Points table
        public bool Insert(PointsBLL pb)
        {

            bool isSucces = false;

            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "INSERT into Points(billAmount,ReceiptNo,pointsAdded,pointsRedeemed,pointBalance,cust_id,date,operations) VALUES (@billAmount,@ReceiptNo,@pointsAdded,@pointsRedeemed,@pointBalance,@cust_id,'"+DateTime.Now+"',@operations) ";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@billAmount",pb.billAmount);
                cmd.Parameters.AddWithValue("@ReceiptNo", pb.ReceiptNo);
                cmd.Parameters.AddWithValue("@pointsAdded", pb.pointsAdded);
                cmd.Parameters.AddWithValue("@pointsRedeemed",pb.pointsRedeemed);
                cmd.Parameters.AddWithValue("@pointBalance",pb.pointBalance);
                cmd.Parameters.AddWithValue("@cust_id", pb.cust_id);
                cmd.Parameters.AddWithValue("@date", pb.date);
                cmd.Parameters.AddWithValue("@operations", pb.operations);

                int row = cmd.ExecuteNonQuery();

                string sql1 = "UPDATE Customers SET PointsAvailable=PointsAvailable + @PointAdded WHERE CustomerID=@CustomerID";
                SqlCommand cmd1 = new SqlCommand(sql1,con);
                //cmd1.Parameters.AddWithValue("@PointsAdded",)
                if (row > 0)
                {
                    //Insertion successful
                    isSucces = true;
                }
                else
                {
                    // Insetion failed
                    isSucces = false;
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

            return isSucces;

        } 
        #endregion



    }
}
