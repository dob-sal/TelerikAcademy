using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace BullsAndCows.WebAPI
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
                name: "Games",
                routeTemplate: "api/games/{action}",
                defaults: new
                {
                    controller = "Games",
                }
            );

            config.Routes.MapHttpRoute(
                name: "Accounts",
                routeTemplate: "api/account/{action}",
                defaults: new
                {
                    controller = "Account",
                }
            );

            config.Routes.MapHttpRoute(
                name: "Scores",
                routeTemplate: "api/scores",
                defaults: new
                {
                    controller = "Scores",
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }
    }
}
