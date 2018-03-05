using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab2FIFA.Startup))]
namespace Lab2FIFA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
