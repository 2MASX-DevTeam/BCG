using BCG_DB.Entity.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tools;

namespace BCG_Manage
{
    public class MvcApplication : System.Web.HttpApplication
    {

        private LoginAttempts db = new LoginAttempts();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Session_Start()
        //{
        //    string ip = IPAddress.GetClientIPAddress(System.Web.HttpContext.Current);
        //    string browserVersion = Request.UserAgent;

        //    var model = new tblIPLoginAttempts()
        //    {
        //        IPAdress = ip,
        //        UserAgend = browserVersion,
        //        DateChanged = DateTime.Now,
        //        DateCreated = DateTime.Now,
        //        UserName = "SYSTEM"
        //    };

        //    db.tblIPLoginAttempts.Add(model);
        //    db.SaveChanges();
        //}
      
    }
}
