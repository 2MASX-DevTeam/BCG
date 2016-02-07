namespace BCG_Manage.Areas.Store.Controllers
{
    using BCG_DB.Entity.Store;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PagedList;
    using System.IO;
    using System.Drawing;
    using System.Data.Entity;
    using Models;
    using System.Configuration;
    using System.Data.SqlClient;
    using BCG_Manage.Controllers;

    public class DiscountsController : BaseController
    {
        // GET: Store/Discounts
        public ActionResult AddNewDiscount()
        {
            return View();
        }

        public ActionResult ViewAllDiscounts()
        {
            return View();
        }

        public ActionResult ApplyDiscountToUsers()
        {
            return View();
        }

        public ActionResult ApplyDiscountToProducts()
        {
            return View();
        }
    }
}