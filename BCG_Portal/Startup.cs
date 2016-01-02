using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BCG_Portal.Startup))]
namespace BCG_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
