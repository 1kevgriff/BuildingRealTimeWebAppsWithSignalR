using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealTime.Web.Startup))]
namespace RealTime.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
