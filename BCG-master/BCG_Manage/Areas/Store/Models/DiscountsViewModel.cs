
namespace BCG_Manage.Areas.Store.Models
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class DiscountsViewModel
    {
        public int IdDiscount { get; set; }

        public int DiscountAmount { get; set; }

        public int CounterUsers { get; set; }

        public int CounterProducts { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class ApplyToUsersViewModel
    {
        public string SelectedDiscount { get; set; }

        public SelectList ListDiscounts { get; set; }
    }

    public class ShopersForDiscountsModel
    {
        public int IdDiscount { get; set; }         

        public int IdShopper { get; set; }

        public string FullName { get; set; }

        public int OrdersCount { get; set; }

        public int DiscountAmount { get; set; }

        public string PeriodDiscount { get; set; }

        public bool IsMessageSended { get; set; }
    }

    public class ProductsForDiscountsModel
    {
        public int IdProduct { get; set; }

        public string Media { get; set; }

        public string ProductName { get; set; }

        public string Price { get; set; }

        public int DiscountAmount { get; set; }

        public int IdDiscount { get; set; }

        public int OrdersCount { get; set; }

        public string PeriodDiscount { get; set; }
        public bool? IsPublish { get; set; }
    }
}