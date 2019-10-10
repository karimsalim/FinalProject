using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackEndASP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(

                name: "AfficheEmploye",
                url: "Employees/{id}",
                new { Controller = "Employees", action = "Index" },
                new { id = @"\d+" }
                );


            routes.MapRoute(
                name: "Default",

                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Employees", action = "listEmployeDebug", id = UrlParameter.Optional }
            );
        }
    }
}
