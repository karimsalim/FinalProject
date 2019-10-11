using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackEndASP.ViewModels;
using DAL;

namespace BackEndASP.Controllers
{
    public class CardsController : Controller
    {
        private BankContext db = new BankContext();

        //[AjaxOnly] //Pour accepter uniquement les appels ajax
        // GET: Cards/Id avec ID l'id du compte courant (Deposit) sélectionné
        public ActionResult Index(int? id)
        {
            ViewBag.IdDeposit = id;
            return View(db.Cards.Include("Deposit").Where(c => c.Deposit.AccountID == id).ToList());
        }

        // GET: Cards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDeposit = id;
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create(int? id)
        {
            ViewBag.IdDeposit = id;
            ViewBag.Url = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View();
        }

        // POST: Cards/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CardId,NewtorkIssuer,CardNumber,SecurityCode,ExpirationDate")] Card card, int? id)
        {
            if (ModelState.IsValid)
            {
                card.Deposit = db.Deposits.Find(id);
                db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Details", "Accounts",new { id = id });
            }
            ViewBag.IdDeposit = id;
            return View("Create", card);
        }

        // GET: Cards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Include("Deposit").First(c => c.CardId == id);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDeposit = card.Deposit.AccountID;
            return View(card);
        }

        // POST: Cards/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Card card, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }
            return View(card);
        }

        // GET: Cards/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCard = id;
            return PartialView("Delete" , card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Card card = db.Cards.Find(id);
            ViewBag.AccountId = card.Deposit.AccountID;
            db.Cards.Remove(card);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = ViewBag.AccountId } );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
