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
    public class AccountsController : Controller
    {
        private BankContext db = new BankContext();

        #region Récupérer la liste des comptes a partir d'un id d'un client => Get: /Account/{id}
        public ActionResult Index(int? id)
        {
            //TODO => INITIALISER LORS DU CLICK CHEZ LE CONTROLLEUR CLIENT
            //ClientIdViewModel PersonID = new ClientIdViewModel { PersonID = id };
            //ViewBag.IdClient = id;
            //return View(db.Accounts.Include("Clients").Where(a => a.Client.PersonId == id).ToList());
            List<Savings> tmpSavings = db.Savings.Where(a => a.Client.PersonId == id).ToList();
            List<Deposit> tmpDeposits = db.Deposits.Where(a => a.Client.PersonId == id).ToList();
            AccountViewModel accountViewModel = new AccountViewModel
            {
                Savings = tmpSavings,
                Deposits = tmpDeposits,
                PersonID = id
            };
            return View(accountViewModel);
        }
        #endregion

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        #region Création d'un compte pour un client => Get : /Account/{id}/Create/{typeCompte}
        public ActionResult Create(int? id, string typecompte)
        {
            ViewBag.IdClient = id;
            ViewBag.typecompte = typecompte;
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSaving([Bind(Include = "BankCode,BranchCode,AccountNumber,Key,IBAN,BIC,Balance," +
            "MinimumAmount,MaximumAmount, InterestRate, MaximumDate")] Savings saving, int? id)
        {
            if (ModelState.IsValid)
            {
                saving.Client = db.Clients.Find(id);
                db.Savings.Add(saving);
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts", new { id = id });
            }

            return View(saving);
        }
        #endregion

        #region Modifier un compte d'un client => Get: /Account/{id}/Edit/{typecompte}/{idcompte}
        public ActionResult Edit(int? id, string typecompte, int? idcompte)
        {
            if (id == null || idcompte == null || typecompte == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditAccountViewModel editAccount = new EditAccountViewModel();
            switch (typecompte)
            {
                case "Saving":
                    editAccount.EditSaving = db.Savings.Find(idcompte);
                    editAccount.PersonID = id;
                    editAccount.TypeCompte = typecompte;
                    return View(editAccount);
                //break;
                case "Deposit":
                    editAccount.EditDeposit = db.Deposits.Find(idcompte);
                    editAccount.PersonID = id;
                    editAccount.TypeCompte = typecompte;
                    return View(editAccount);
                    //break;
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    //break;
            }
            Account account = db.Accounts.Find(idcompte);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,BankCode,BranchCode,AccountNumber,Key,IBAN,BIC,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }
        #endregion

        #region Suppression d'un compte client
        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Méthode dispose
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
