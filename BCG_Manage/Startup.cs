using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BCG_Manage.Startup))]
namespace BCG_Manage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
