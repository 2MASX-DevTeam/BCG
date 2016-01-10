using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Manage.Controllers
{
    [Authorize]
    public class ManageSIteController : Controller
    {
        // GET: ManageSIte
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}