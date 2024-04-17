using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Herbal_Garden.Startup))]
namespace Herbal_Garden
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
