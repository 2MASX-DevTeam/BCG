using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCG_Portal.Models
{


    public class ProductsViewModels
    {
        public List<Product> LstProducts = new List<Product>();
    }

    public class Product
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string ImageUrl { get; set; }

        public string Media { get; set; }
    }
}