using System.Web.Mvc;

namespace BCG_Manage.Areas.BCGenerator
{
    public class BCGeneratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BCGenerator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
   

            context.MapRoute(
            "BCGenerator_default2",
            "BCGenerator/{action}/{id}",
            new { controller = "Main", action = "AddNewCard", area = "BCGenerator", id = UrlParameter.Optional }
        );

            context.MapRoute(
       "BCGenerator_default",
       "BCGenerator/{action}",
       new { controller = "Main", action = "CurvedEdgesPartial", area = "BCGenerator" }
   );
        }
    }
}