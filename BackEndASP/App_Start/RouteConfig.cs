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
                name: "AfficheComptes",
                url: "Accounts/{id}",
                new { Controller = "Accounts", action = "Index" },
                new { id = @"\d+" });

            routes.MapRoute(
                name: "CreerCompte",
                url: "Accounts/{id}/Create/{typecompte}",
                new { Controller = "Accounts", action = "Create" },
                new { id = @"\d+", typecompte = @"(Deposit|Saving)$" });

            routes.MapRoute(
                name: "EditCompte",
                url: "Account/{id}/Edit/{typecompte}/{idcompte}",
                new { Controller = "Accounts", action = "Edit" },
                new { id = @"\d+", typecompte = @"(Deposit|Saving)$", idcompte = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
