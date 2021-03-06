﻿using System.Web;
using System.Web.Optimization;

namespace BCG_Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/jquery-ui-1.11.4.min.js",
                      "~/Scripts/jquery-eu-cookie-law-popup.js",
                      "~/Scripts/bussinesCardJs.js",
                      "~/Scripts/parallaxjs.js",
                      "~/Scripts/custPortalScripts.js",
                      "~/Scripts/jquery.elastislide.js",
                      "~/Scripts/jquerypp.custom.js",
                      "~/Scripts/modernizr.custom.17475.js",
                      "~/Scripts/custQ.js"
                   ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/custPortalCss.css",
                      "~/Content/custstuff.css",
                      "~/Content/bussinessCardsStyle.min.css",
                      "~/Content/custAuthenticateCss.css",
                       "~/Content/parallaxcss.css",
                       "~/Content/jquery-eupopup.min.css",
                       "~/Content/custCarouselCss.min.css",
                       "~/Content/custCarouselDemo.min.css",
                       "~/Content/custElastislideCarousel.min.css",
                       "~/fonts/font-awesome-4.6.3/css/font-awesome.min.css"
                       ));
        }
    }
}