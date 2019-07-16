using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyalty.BLL
{
    class PointsBLL
    {
        public int point_id { get; set; }
        public string billAmount { get; set; }
        public string ReceiptNo { get; set; }
        public string pointsAdded { get; set; }
        public string pointsRedeemed { get; set; }
        public string pointBalance { get; set; }
        public int cust_id { get; set; }
        public string date { get; set; }
        public string operations { get; set; }
    }
}
