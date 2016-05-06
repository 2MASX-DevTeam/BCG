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
    using System.Net;
    using Newtonsoft.Json;

    [Authorize]
    public class ShoppersController : BaseController
    {

        [HttpGet]
        public ActionResult RegisterShopper()
        {

            return View();
        }


        public ActionResult ImageUploaderHandler(string iduser)
        {
            throw new NotImplementedException();
        }
    }
}