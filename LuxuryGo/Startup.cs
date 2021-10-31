using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LuxuryGo.Startup))]
namespace LuxuryGo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
