namespace BCG_Manage.Areas.BCGenerator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using BCG_Manage.Controllers;

    public class MainController : BaseController
    {

        [HttpGet]
        public ActionResult AddNewCard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SharpEdgesPartial()
        {
            return PartialView("~/Areas/BCGenerator/Views/Main/_SharpEdgesFormPartial.cshtml");
        }

        [HttpGet]
        public ActionResult CurvedEdgesPartial()
        {
            return PartialView("~/Areas/BCGenerator/Views/Main/_CurvedEdgesFormPartial.cshtml");
        }
    }
}