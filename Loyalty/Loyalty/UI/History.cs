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
    public partial class History : Form
    {

        ReceiptDAL rd = new ReceiptDAL();
        ReceiptBLL rb = new ReceiptBLL();
        public History()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void History_Load(object sender, EventArgs e)
        {
            DataTable dt = rd.Select();
            dataGridView1.DataSource = dt;

        }
    }
}
