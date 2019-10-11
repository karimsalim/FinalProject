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

            #region Route Liste des comptes d'un client 
            routes.MapRoute(
                name: "AfficheComptes",
                url: "Accounts/{id}",
                new { Controller = "Accounts", action = "Index" },
                new { id = @"\d+" });
            #endregion

            #region Route Suppression d'un compte
            routes.MapRoute(
                name: "DeleteAccount",
                url: "Accounts/Delete/{id}/{typecompte}",
                new { Controller = "Accounts", action = "Delete" },
                new { id = @"\d+", typecompte = @"(Deposit|Saving)$" });
            #endregion

            #region Route Creation d'un compte
            routes.MapRoute(
                name: "CreerCompte",
                url: "Accounts/{id}/Create/{typecompte}",
                new { Controller = "Accounts", action = "Create" },
                new { id = @"\d+", typecompte = @"(Deposit|Saving)$" });
            #endregion

            #region Route Modification d'un compte
            routes.MapRoute(
                name: "EditCompte",
                url: "Account/{id}/Edit/{typecompte}/{idcompte}",
                new { Controller = "Accounts", action = "Edit" },
                new { id = @"\d+", typecompte = @"(Deposit|Saving)$", idcompte = @"\d+" });
            #endregion

            #region Route Détails d'un compte
            routes.MapRoute(
                name:"DetailsAccounts",
                url: "Accounts/{id}/Details/{typecompte}/{idcompte}",
                new { Controller = "Accounts", action = "Details" },
                new { id = @"\d+", typecompte = @"(Deposit|Saving)$", idcompte = @"\d+" });
            #endregion

            #region Route par Défaut <= Peut être à modifier au futur !
            routes.MapRoute(
                name: "Accueil",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            #endregion
        }
    }
}
