using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyBuyCR.Startup))]
namespace EasyBuyCR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
