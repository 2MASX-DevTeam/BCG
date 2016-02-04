using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCG_Manage.Areas.Store.Models
{
    public class OrdersViewModel
    {
        public int IdOrder { get; set; }

        public string UserFullName { get; set; }

        public int Quantity { get; set; }

        public int QuantityLeft { get; set; }

        public  string Product { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Status { get; set; }

        public string Price { get; set; }

        public decimal Currencie { get; set; }

        public int Discount { get; set; }
        
    }
    
}