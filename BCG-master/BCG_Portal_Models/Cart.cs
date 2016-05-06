using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCG_Portal_Models
{
       public class Cart
    {
        [Key]
        public int      ProductId    { get; set; }
        public string   CartId      { get; set; }
        public int      Counter       { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
