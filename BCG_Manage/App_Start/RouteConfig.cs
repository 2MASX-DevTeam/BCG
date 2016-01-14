using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BCG_Manage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Manage


            routes.MapRoute(
                "Admin",
                "Admin",
                new  { controller = "ManageSite", action = "Index" }
                );


            #endregion

            #region Multilanguage
            routes.MapRoute(
           "AddLanguage",
           "Add Language",
           new { controller = "MultiLanguage", action = "AddNewLanguage"}
           );

            routes.MapRoute(
         "ViewAllLanguages",
         "View Languages/{id}",
         new { controller = "MultiLanguage", action = "ViewAllLanguages", id = UrlParameter.Optional }
         );

            #endregion
            routes.MapRoute(
               name: "Default2",
               url: "{controller}/{action}/{id}",
               defaults: new
               {
                   controller = "Languages",
                   action = "Index",
                   id = UrlParameter.Optional
               }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
