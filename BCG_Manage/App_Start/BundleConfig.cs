using System.Web;
using System.Web.Optimization;

namespace BCG_Manage
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
            // ready for production, use the ¬build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/plugins/morris/morris.min.js",
                      "~/plugins/sparkline/jquery.sparkline.min.js",
                      "~/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/plugins/knob/jquery.knob.js",
                      "~/plugins/daterangepicker/daterangepicker.js",
                      "~/plugins/datepicker/bootstrap-datepicker.js",
                      "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/plugins/fastclick/fastclick.min.js",
                      "~/dist/js/app.min.js",
                      //"~/dist/js/pages/dashboard.js",
                      //"~/dist/js/demo.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/dist/css/AdminLTE.min.css",
                "~/dist/css/skins/_all-skins.min.css",
                "~/plugins/iCheck/flat/blue.css",
                "~/plugins/morris/morris.css",
                "~/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                "~/plugins/datepicker/datepicker3.css",
                "~/plugins/daterangepicker/daterangepicker-bs3.css",
                "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
