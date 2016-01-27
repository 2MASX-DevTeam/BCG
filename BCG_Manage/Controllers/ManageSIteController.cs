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
using BCG_DB.Entity.Manage;
using System.Collections.Generic;
using System.Configuration;

namespace BCG_Manage.Controllers
{
    [Authorize]
    public class ManageSIteController : BaseController
    {
        private MultiLanguageModel db = new MultiLanguageModel();
        private ApplicationDbContext dbUsers = new ApplicationDbContext();
        private LoginAttempts Ips = new LoginAttempts();


        public ActionResult Index()
        {
            var model = new IndexManageSiteViewModel();
            var settedDays = Convert.ToInt32(ConfigurationManager.AppSettings["IndexCountersPeriodDays"]);
            model.SettedDays = Math.Abs(settedDays);

            try
            {
                var lang = db.tblLanguages.Where(m => m.IsActive == true);

                DateTime yest = DateTime.UtcNow.Date.AddDays(settedDays);

                var uniqueCounter = (from tbllogin in Ips.tblIPLoginAttempts
                                     where tbllogin.DateCreated >= yest
                                     group tbllogin by tbllogin.IPAdress into unique
                                     select unique.FirstOrDefault().IPAdress).Count();

                var registraionCounter = (from tblUsers in dbUsers.Users
                                          where tblUsers.DateCreated >= yest
                                          select tblUsers.Email).Count();

                model.UniqueVisitorsCounter = uniqueCounter;
                model.UserRegistrationCounter = registraionCounter;
                foreach (var row in lang)
                {
                    model.listLanguages.Add(new ListOfActiveLanguages()
                    {
                        IdLanguage = row.IdLanguage,
                        Language = row.Language,
                        Culture = row.Culture,
                        Initials = row.Initials
                    });
                }
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in loading from DataBase";
            }
            return View(model);
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

            var model = new List<UniqueVisitorsModel>();

            DateTime yest = DateTime.UtcNow.Date.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["IndexCountersPeriodDays"]));

            var ips = from tbllogin in Ips.tblIPLoginAttempts
                      where tbllogin.DateCreated >= yest
                      group tbllogin by tbllogin.IPAdress into unique
                      select unique.FirstOrDefault().IPAdress;

            foreach (var ip in ips)
            {
                if (!String.IsNullOrEmpty(ip))
                {
                    var fullinfo = GetCountryByIp(ip.ToString());

                    if (fullinfo == null)
                        continue;

                    model.Add(new UniqueVisitorsModel
                    {
                        lat = fullinfo.latitude,
                        lng = fullinfo.longitude,
                        country = fullinfo.country_name
                    });
                }

            }

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