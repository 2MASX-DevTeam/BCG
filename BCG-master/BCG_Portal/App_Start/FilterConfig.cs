using System.Web;
using System.Web.Mvc;

namespace BCG_Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            /* The Authorize filter prevents anonymous users from accessing any methods in the application.  The RequireHttps requires that all access to the web app be through HTTPS. */
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
