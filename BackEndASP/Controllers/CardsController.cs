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

        #region Liste des cartes d'un compte courant => Get : Cards/{id}
        public ActionResult Index(int? id)
        {
            ViewBag.IdDeposit = id;
            return View(db.Cards.Include("Deposit").Where(c => c.Deposit.AccountID == id).ToList());
        }
        #endregion

        #region Détails d'une carte de crédit ==> NON DISPONIBLE DANS NOTRE CAS ! A SUPPRIMER PLUS TARD
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
        #endregion

        #region Création D'une carte de crédit => Get: Cards/Create/{idcompte}
        public ActionResult Create(int? id)
        {
            ViewBag.IdDeposit = id;
            ViewBag.Url = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View();
        }
        #endregion

        #region Sauvegarde de création d'une carte => Post : Cards/Create/{idcompte}
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
        #endregion

        #region Modification d'une carte de crédit => Get : Cards/Edit/{idcard}
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
        #endregion

        #region Sauvegarde d'une modification d'une carte => Post : Cards/Edit/{idcard}
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
        #endregion

        #region Suppression d'une carte de crédit => Post : Cards/Delete/{idcard}
        [HttpPost]
        public ActionResult Delete(int? id)
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
            ViewBag.idCard = id;
            ViewBag.idCompte = card.Deposit.AccountID;
            ViewBag.idClient = card.Deposit.Client.PersonId;
            return PartialView("Delete" , card);
        }
        #endregion

        #region Confirmation de suppression d'une carte de crédit => Post : Cards/Delete/{idcard}
        [HttpPost, ActionName("DeleteCard")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idCompte, int idClient)
        {
            Card card = db.Cards.Find(id);
            //ViewBag.AccountId = card.Deposit.AccountID;
            db.Cards.Remove(card);
            db.SaveChanges();
            return RedirectToRoute("DetailsAccounts",new { id = idClient, typecompte = "Deposit", idcompte = idCompte });//("Index", new { id = ViewBag.AccountId } );
        }
        #endregion

        #region Dispose du controller Cards
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
