using Loyalty.BLL;
using Loyalty.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loyalty.UI
{
    public partial class Login : Form
    {

        adminLoginBLL alb = new adminLoginBLL();
        adminLoginDAL ald = new adminLoginDAL();
      public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            alb.username = txtUsername.Text.Trim();
            alb.password = txtPassword.Text.Trim();

            //Check login credentials
            bool success = ald.adminLoginCheck(alb);

            if (success == true)
            {
                MessageBox.Show("Login Successfull");
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or Password is Invalid");
                Clear();
            }

        }
    }
}