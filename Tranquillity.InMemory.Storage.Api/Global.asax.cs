using Tranquillity.InMemory.Storage.Injection;
using System.Web.Http;

namespace Tranquillity.InMemory.Storage.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            this.SetupDependencyInjection();
        }

        private void SetupDependencyInjection()
        {
            var injectionStartup = new InjectionStartup();
            injectionStartup.InitializeComponents();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(injectionStartup.Container);
        }
    }
}