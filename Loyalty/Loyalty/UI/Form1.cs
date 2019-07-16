using Loyalty.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loyalty
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addNewCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCustomer anc = new AddNewCustomer();
            anc.Show();
        }

        private void pointManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoyaltyManagement lm = new LoyaltyManagement();
            lm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            timer1.Start();
            label9.Text = DateTime.Now.ToLongTimeString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void addCustomerBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void addPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPoints ap = new AddPoints();
            ap.Show();
        }

        private void redeemPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedeemPoints rp = new RedeemPoints();
            rp.Show();
        }

        private void pointHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History h = new History();
            h.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCustomer anc = new AddNewCustomer();
            anc.Show();
        }
    }
}
