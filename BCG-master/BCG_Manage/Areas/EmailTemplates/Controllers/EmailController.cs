using BCG_Manage.Areas.EmailTemplates.Models;
using BCG_Manage.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Manage.Areas.EmailTemplates.Controllers
{
    public class EmailController : BaseController
    {
        public EmailModels model = new EmailModels();
        // GET: EmailTemplates/Email
        public  ActionResult RegistrationEmail()
        {
            model = (EmailModels)ViewData.Model;

            return View(model);
        }

        
    }
}