using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevFestEvent.Startup))]
namespace DevFestEvent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
