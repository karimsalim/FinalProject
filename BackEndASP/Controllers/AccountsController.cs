﻿using System;
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
        #endregion

        #region Sauvegarde de la création d'un compte epargne
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSaving([Bind(Include = "BankCode,BranchCode,AccountNumber,Key,IBAN,BIC,Balance," +
            "MinimumAmount,MaximumAmount, InterestRate, MaximumDate")] Savings saving, int? id)/**/
        {
            if (ModelState.IsValid)
            {
                saving.Client = db.Clients.Find(id);
                db.Savings.Add(saving);
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts", new { id = id });
            }
            AccountViewModel account = new AccountViewModel();
            account.Savings.Add(saving);
            ViewBag.IdClient = id;
            ViewBag.typecompte = "Saving";
            return View("Create", account);
        }
        #endregion

        #region Sauvegarde de la création d'un compte courant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeposit([Bind(Include = "BankCode, BranchCode,AccountNumber,Key,IBAN,BIC,Balance," +
            "GestionDate, AutorizedOverdraft,FreeOverdraft,OverdraftChargeRate")] Deposit deposit, int? id)
        {
            if(ModelState.IsValid)
            {
                deposit.Client = db.Clients.Find(id);
                db.Deposits.Add(deposit);
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts", new { id = id });
            }
            AccountViewModel account = new AccountViewModel();
            account.Deposits.Add(deposit);
            ViewBag.IdClient = id;
            ViewBag.typecompte = "Deposit";
            return View("Create", account);
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
            //Account account = db.Accounts.Find(idcompte);
            //if (account == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(account);
        }
        #endregion

        #region Sauvegarder la modification d'un compte epargne
        //Modification d'un compte de type Saving avec une requête POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSaving( EditAccountViewModel account, int? id)/*[Bind(Include = "AccountID,BankCode,BranchCode,AccountNumber,Key,IBAN,BIC,Balance," +
            "MinimumAmount,MaximumAmount,InterestRate,MaximumDate")]*/
        {
            if (ModelState.IsValid)
            {
                db.Entry(account.EditSaving).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts",new { id = id, Edit ="Success" });
            }
            return View("Edit", account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDeposit(EditAccountViewModel account, int? id)
        {
            if(ModelState.IsValid)
            {
                db.Entry(account.EditDeposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts", new { id = id, Edit = "Success" });
            }
            account.EditSaving = null;
            return View("Edit",account);
        }
        #endregion

        #region Suppression d'un compte client
        // GET: Accounts/Delete/5
        [HttpPost]

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
            ViewBag.idCompte = id;
            return PartialView(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("DeleteSaving")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            ViewBag.IdClient = account.Client.PersonId;
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = ViewBag.IdClient });
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
