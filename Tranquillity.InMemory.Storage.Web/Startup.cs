using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tranquillity.InMemory.Storage.Web.Startup))]
namespace Tranquillity.InMemory.Storage.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
