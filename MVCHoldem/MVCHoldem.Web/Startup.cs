using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCHoldem.Web.Startup))]
namespace MVCHoldem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
