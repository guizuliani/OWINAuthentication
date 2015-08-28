using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OWINAuthentication.Startup))]

namespace OWINAuthentication
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthentication(app);
        }

    }
}
