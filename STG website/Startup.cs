using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(STG_website.Startup))]
namespace STG_website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
