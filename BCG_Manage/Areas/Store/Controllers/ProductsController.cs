using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Manage.Areas.Store.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Store/Products
        public ActionResult Index()
        {
            return View();
        }
    }
}