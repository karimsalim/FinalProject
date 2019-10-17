using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiAngular
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Transfert",
                routeTemplate: "api/Clients/PutTransfert/{id}/{rib}/{montantOperation}",
                defaults: new { controller = "Clients", action = "PutTransfert" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
