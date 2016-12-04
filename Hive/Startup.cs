using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hive.Startup))]
namespace Hive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}