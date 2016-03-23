﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCG_Portal.Models.BCGModels;
namespace BCG_Portal.Controllers
{
    public class HomeController : Controller
    {
        /* Private and Static naming have the same effect */
        private readonly BCG_Portal.Models.BCGModels.tblShopper _shopper =
                new BCG_Portal.Models.BCGModels.tblShopper();

        private static BCG_Portal.Models.BCGModels.tblIPLoginAttempt _ipGeoLocation =
                new BCG_Portal.Models.BCGModels.tblIPLoginAttempt();
        public ActionResult Index()
        {
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
        public ActionResult Products()
        {
            return View();
        }

    }
}