using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Festispec.WebApp.Startup))]
namespace Festispec.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
