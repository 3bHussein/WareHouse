using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppication.Startup))]
namespace WebAppication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
