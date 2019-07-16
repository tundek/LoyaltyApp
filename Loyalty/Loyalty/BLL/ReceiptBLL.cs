using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyalty.BLL
{
    class ReceiptBLL
    {
        public int receipt_id { get; set; }
        public string ReceiptNo { get; set; }
        public double BillAmount { get; set; }
        public double PointsAdded { get; set; }
        public string Operation { get; set; }
        public double PointsRedeemed { get; set; }
        public string CustomerID { get; set; }
        public string date { get; set; }

    }
}
