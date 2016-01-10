namespace BCG_Manage.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using BCG_DB;
    using BCG_DB.Entity.Manage;
    using Tools;
    public class HomeController : Controller
    {
        private LoginAtempts db = new LoginAtempts();

        [HttpGet]
        public ActionResult Index()
        {
          string ip =   IPAddress.GetClientIPAddress(System.Web.HttpContext.Current);

            var model = new tblIPLoginAtemts() {
                IPAdress = ip,
                DateChanged= DateTime.Now,
                DateCreated = DateTime.Now,
                UserName = "SYSTEM"
            };

            db.tblIPLoginAtemts.Add(model);
            db.SaveChanges();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}