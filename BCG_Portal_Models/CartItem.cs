using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCG_Portal_Models
{
    public class CartItem
    {
      [Key]
      public string ItemId { get; set; }
    
      public string CartId { get; set; }
    
      public int Quantity { get; set; }
    
      public DateTime DateCreated { get; set; }
    
      public int ProductId { get; set; }
    
      public virtual Product Product { get; set; }
    
    }
}
