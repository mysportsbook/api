using System.Web.Http;

namespace MySportsBook.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "ApiById",
                routeTemplate: "api/Get/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{venueid}/{sportid}/{courtid}/{batchid}/{playerid}/{date}",
                defaults: new { venueid = RouteParameter.Optional, sportid = RouteParameter.Optional, courtid = RouteParameter.Optional, batchid = RouteParameter.Optional, playerid = RouteParameter.Optional, Date = RouteParameter.Optional }
            );
        }
    }
}
