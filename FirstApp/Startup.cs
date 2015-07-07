using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstApp.Startup))]
namespace FirstApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
