using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    public class HomeController : Controller
    {
        private BankContext db = new BankContext();


        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var claim = User.Identity as ClaimsIdentity;
                if (claim != null)
                {
                    int id = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value);
                    Employee employee = db.Employees.Find(id);
                    return RedirectToAction("Index", "Employees", new { id = id });
                }
                else
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}