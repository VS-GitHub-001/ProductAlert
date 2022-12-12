using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductAlert.Startup))]
namespace ProductAlert
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
