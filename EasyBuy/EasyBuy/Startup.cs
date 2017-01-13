using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyBuy.Startup))]
namespace EasyBuy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
