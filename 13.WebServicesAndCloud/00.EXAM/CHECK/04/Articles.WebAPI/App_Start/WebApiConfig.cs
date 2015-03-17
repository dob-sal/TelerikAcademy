namespace Articles.WebAPI
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.OData.Extensions;
    using Microsoft.Owin.Security.OAuth;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            config.EnableCors(new EnableCorsAttribute("*","*","*"));
            config.AddODataQueryFilter();


            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            

            ////    api/games/GAME_ID/guess
            //config.Routes.MapHttpRoute(
            //    name: "Guess",
            //    routeTemplate: "api/games/{id}/guess",
            //    defaults: new 
            //    { 
            //        controller = "Guess" 
            //    });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
