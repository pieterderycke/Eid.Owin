using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eid.Owin.DemoWebApp.Startup))]
namespace Eid.Owin.DemoWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
