using System.Web;
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
                      "~/Scripts/bussinesCardJs.js",
                      "~/Scripts/parallaxjs.js",
                      "~/Scripts/custPortalScripts.js",
                      "~/Scripts/jquery-eu-cookie-law-popup.js",
                      "~/Scripts/jquery-ui-1.11.4.min.js",
                      "~/Scripts/jquery.elastislide.js",
                      "~/Scripts/jquerypp.custom.js",
                      "~/Scripts/modernizr.custom.17475.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bussinessCardsStyle.min.css",
                       "~/Content/parallaxcss.min.css",
                       "~/Content/custPortalCss.min.css",
                       "~/Content/cuts-jquery-eupopup.min.css",
                       "~/Content/custCarouselCss.min.css",
                       "~/Content/custCarouselDemo.min.css",
                       "~/Content/custElastislideCarousel.min.css"));
        }
    }
}
