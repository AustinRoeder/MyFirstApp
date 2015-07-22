using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmurfsProject.Startup))]
namespace SmurfsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
