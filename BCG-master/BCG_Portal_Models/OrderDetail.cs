using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCG_Portal_Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
