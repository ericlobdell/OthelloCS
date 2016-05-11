using Microsoft.Owin;
using Owin;

[assembly: OwinStartup( typeof( OthelloCS.Web.Startup ) )]

namespace OthelloCS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
