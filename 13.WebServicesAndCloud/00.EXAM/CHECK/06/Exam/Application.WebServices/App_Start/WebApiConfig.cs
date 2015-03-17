namespace Application.WebServices
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.OData.Extensions;

    using Microsoft.Owin.Security.OAuth;
    using Newtonsoft.Json;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.AddODataQueryFilter();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

              config.Routes.MapHttpRoute("Users",
                  "api/account/{action}", new { Controller = "Account" });

             config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            
             config.Routes.MapHttpRoute(
             name: "GameByid",
             routeTemplate: "api/games/id",
             defaults: new { Controller = "Game" });  

        /*    config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter); */
        }
    }
}