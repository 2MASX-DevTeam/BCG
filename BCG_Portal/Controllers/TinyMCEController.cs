using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCG_Portal.Controllers.Application_Controllers;
using BCG_Portal.Models;

namespace BCG_Portal.Controllers
{
    public class TinyMCEController : BaseController
    {
        // An action to display your TinyMCE editor
        public ActionResult Index()
        {
            return View();
        }

        // An action that will accept your Html Content
        [HttpPost]
        public ActionResult Index(ExampleClass model)
        {
            return View();
        }
    }
}