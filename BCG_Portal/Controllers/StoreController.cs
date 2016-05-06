using BCG_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Portal.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult AddToCard()
        {
            var model = new ProductsViewModels();

            return PartialView("~/Views/Shared/_UserCard.cshtml", model);
        }

        // GET: Store
        public ActionResult CheckOut()
        {
            return View();
        }
    }
}