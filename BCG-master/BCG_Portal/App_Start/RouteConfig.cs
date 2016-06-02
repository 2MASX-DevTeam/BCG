using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BCG_Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                 name: "DefaultBG",
                 url: "{controller}/{action}/{id}",
                 defaults: new
                 {
                     controller = "Home",
                     action = "IndexBG",
                     id = UrlParameter.Optional
                 }
             );

            routes.MapRoute(
                name: "DefaultEN",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
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
               action = "Indexs",
               id = UrlParameter.Optional
           }
       );



        }
    }
}
