using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Portal.Models
{
    public class UserCard
    {
        public List<Card> Products = new List<Card>();

        public int Total { get; set; }
    }

    public class Card
    {
        public int IdShopingCard { get; set; }

        public int IdProduct { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public string Price { get; set; }

    }

    public class ProductsViewModels
    {
        public List<Product> LstProducts = new List<Product>();

        public string SelectedQuantity { get; set; }

        public SelectList ListQuantitys { get; set; }

        public int IdProduct { get; set; }
    }

    public class Product
    {

        public int IdProduct { get; set; }
        public string Name { get; set; }

        public string Price { get; set; }

        public string ImageUrl { get; set; }

        public string Media { get; set; }
    }
}