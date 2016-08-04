using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProducePricingFrontEnd.Startup))]
namespace ProducePricingFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
