﻿using System;
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
               "EditLanguage",
               "Edit Language",
               new { controller = "MultiLanguage", action = "EditLanguage" }
               );

            routes.MapRoute(
         "ViewAllLanguages",
         "View Languages/{id}",
         new { controller = "MultiLanguage", action = "ViewAllLanguages", id = UrlParameter.Optional }
         );



            routes.MapRoute(
           "AddResource",
           "Add Resource",
           new { controller = "MultiLanguage", action = "AddNewResource" }
           );


            routes.MapRoute(
           "ViewAllResources",
           "View All Resources",
           new { controller = "MultiLanguage", action = "ViewAllResources" }
           );

            routes.MapRoute(
          "Edit Resource",
          "Edit Resource",
          new { controller = "MultiLanguage", action = "EditResource" }
          );

            routes.MapRoute(
       "AddTranslationResource",
       "Add translation to resource",
       new { controller = "MultiLanguage", action = "AddTranslationResource" }
       );


            routes.MapRoute(
       "AddNewStaticText",
       "Add New Static Text",
       new { controller = "MultiLanguage", action = "AddNewStaticText" }
       );


            routes.MapRoute(
       "EditStaticText",
       "Edit Static Text",
       new { controller = "MultiLanguage", action = "EditStaticText" }
       );


            routes.MapRoute(
       "VewAllStaticTexts",
       "Vew All Static Texts",
       new { controller = "MultiLanguage", action = "ViewAllStaticTexts" }
       );


            routes.MapRoute(
       "AddTranslationStaticText",
       "Add Translation Static Text",
       new { controller = "MultiLanguage", action = "AddTranslationStaticText" }
       );
            #endregion

            #region Roles

            routes.MapRoute(
             "Roles",
             "Create role",
             new { controller = "Roles", action = "Create" }
             );


            routes.MapRoute(
           "ManageRoles",
           "Manage roles",
           new { controller = "Roles", action = "ManageUserRoles" }
           );


            routes.MapRoute(
                       "ViewRoles",
                       "View roles",
                       new { controller = "Roles", action = "ViewAllRoles" }
                       );

            routes.MapRoute(
                  "EditRoles",
                  "Edit role",
                  new { controller = "Roles", action = "EditRole" }
                  );
            #endregion

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
