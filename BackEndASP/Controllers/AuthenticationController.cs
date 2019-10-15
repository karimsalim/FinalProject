using BackEndASP.Models;
using DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace BackEndASP.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Connexion au système
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region Validation de la connexion au système
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            DefEmp emp = ValidateUser(viewModel.Login, viewModel.Password);
            if (!emp.find)
            {
                ModelState.AddModelError(string.Empty, "Le nom d'utilisateur ou le mot de passe est incorrect!");
                return View(viewModel);
            }
            // L'authentification est réussie, 
            // injecter l'identifiant utilisateur dans le cookie d'authentification :
            var loginClaim = new Claim(ClaimTypes.NameIdentifier, emp.id.ToString());
            var claimsIdentity = new ClaimsIdentity(new[] { loginClaim }, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(claimsIdentity);

            // Rediriger vers l'URL d'origine :
            //if (Url.IsLocalUrl(ViewBag.ReturnUrl))
            //    return Redirect(ViewBag.ReturnUrl);
            // Par défaut, rediriger vers la page d'accueil :
            return RedirectToAction("Index", "Home", new { id = emp.id });
        }
        #endregion

        #region Deconnexion au système
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Validation du pseudo et du mot de passe
        private DefEmp ValidateUser(string Login, string Password)
        {
            using (BankContext db = new BankContext())
            {
                string hash = HashPassword(Password);
                List<Employee> emp = db.Employees.Where(e => e.Pseudo == Login).Where(e => e.Password == hash).ToList();
                DefEmp employe;
                if (emp.Count == 1)
                {
                    employe = new DefEmp()
                    {
                        find = true,
                        id = emp[0].PersonId
                    };
                    return employe;
                }
                employe = new DefEmp()
                {
                    find = false
                };
                return employe;
            }

        }
        #endregion

        #region Hashage du Password
        /// <summary>
        /// Permet de hasher un mot de passe saisi par l'utilisateur
        /// </summary>
        /// <param name="password">Mot de passe saisi en clair</param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            using (SHA256CryptoServiceProvider sHA = new SHA256CryptoServiceProvider())
            {
                return BitConverter.ToString(sHA.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }
        #endregion
    }

    #region Classe d'un employé identifé
    internal class DefEmp
    {
        public bool find { get; set; }

        public int id { get; set; }
    }
    #endregion
}