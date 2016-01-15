using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BCG_Manage.Models;
using BCG_DB.Entity.MultiLanguageEntity;

namespace BCG_Manage.Controllers
{
    [Authorize]
    public class ManageSIteController : Controller
    {
        private MultiLanguageModel db = new MultiLanguageModel();

        public ActionResult Index()
        {
            return View(db);
        }
     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}