using Tranquillity.InMemory.Storage.Api.App_Start;
using System.Web.Http;

namespace Tranquillity.InMemory.Storage.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new AllowOptionsHandler());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{nameSpace}/{key}/",
                defaults: new { nameSpace = RouteParameter.Optional, key = RouteParameter.Optional}
            );
        }
    }
}
