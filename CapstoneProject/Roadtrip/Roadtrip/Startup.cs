using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Roadtrip.Startup))]
namespace Roadtrip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
