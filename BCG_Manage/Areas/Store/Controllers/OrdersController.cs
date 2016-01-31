using BCG_DB.Entity.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Manage.Areas.Store.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private tblCompleteOrder tblComOrd = new tblCompleteOrder();

        // GET: Store/Orders
        public ActionResult ViewAll()
        {
            return View();
        }

        public ActionResult NotConfirmed()
        {
            return View();
        }
    }
}