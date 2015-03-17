using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace ServicesExam.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "Next",
               routeTemplate: "api/{controller}/{action}",
           defaults : new {action = "Next" }
           );

            config.Routes.MapHttpRoute(
                name: "JoinGame",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Games", action = "Join", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name: "GetDetails",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { controller = "Games", action = "GetGameDetails", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
                name: "AllAvailableGames",
                routeTemplate: "api/{controller}/{page}",
                defaults: new { controller = "Games", action = "AllFreeToJoinGames", page = RouteParameter.Optional }
            );

         

            config.Routes.MapHttpRoute(
                name: "MakeAGuess",
                routeTemplate: "api/games/{id}/{action}",
                defaults: new { controller = "Guesses", action = "Guess"}
            );

            config.EnableCors(new EnableCorsAttribute("*","*","*"));
        }
    }
}
