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
using System.Threading;

namespace BCG_Manage.Controllers
{
    [Authorize]
    public class ManageSIteController : BaseController
    {
        private MultiLanguageModel db = new MultiLanguageModel();
        private ApplicationDbContext dbUsers = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View(db);
        }
        
        public ActionResult GetSideBarPartial()
        {
            var model = new LayoutModels();
            model.CountAllRoles = dbUsers.Roles.Count();
            model.CountAllUsers = dbUsers.Users.Count();
         
            var partial = PartialView("_MainSidebarPartial", model);

            return partial;
        }

        public ActionResult UniqueVisitors()
        {
            var model = new UniqueVisitorsModel();

            return PartialView("~/Views/Shared/IndexPartials/_UniqueVisitorsPartial.cshtml", model);
        }

        public ActionResult UserRegistrations()
        {
            var model = new UserRegistrationsModel();

            return PartialView("~/Views/Shared/IndexPartials/_UserRegistrationsPartial.cshtml", model);
        }


        public ActionResult NewOrders()
        {
            var model = new NewOrdersModel();

            return PartialView("~/Views/Shared/IndexPartials/_LatestOrdersPartial.cshtml", model);
        }
    }
}