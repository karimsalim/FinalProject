﻿using System;
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
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ModifClient",
                routeTemplate: "api/Clients/PutClient/{id}",
                defaults: new { controller = "Clients", action = "PutClient" });

            config.Routes.MapHttpRoute(
                name: "Transfert",
                routeTemplate: "api/Clients/PutTransfert/{id}/{rib}/{montantOperation}",
                //routeTemplate: "api/Clients/PutTransfert",
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