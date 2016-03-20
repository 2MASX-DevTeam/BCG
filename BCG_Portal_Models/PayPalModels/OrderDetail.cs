using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCG_Portal_Models.PayPalModels
{
    public class OrderDetail
  {
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public string Username { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double? UnitPrice { get; set; }

  }
}
