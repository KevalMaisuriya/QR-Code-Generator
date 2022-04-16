using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QRGenerator.Startup))]
namespace QRGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
