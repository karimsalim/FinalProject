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

        #region Details des comptes du client
        public ActionResult Details(int? id, string typecompte, int? idCompte)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            switch (typecompte)
            {
                case "Deposit":
                    EditAccountViewModel deposit = new EditAccountViewModel()
                    {
                        EditDeposit = db.Deposits
                            .Include("Cards").First(d => d.AccountID == idCompte),
                        PersonID = id,
                        TypeCompte = typecompte
                    };
                    if (deposit.EditDeposit == null){
                        return HttpNotFound();
                    }
                    deposit.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
                    return View(deposit);
                case "Saving":
                    EditAccountViewModel saving = new EditAccountViewModel()
                    {
                        EditSaving = db.Savings.Find(idCompte),
                        PersonID = id,
                        TypeCompte = typecompte
                    };
                    if (saving.EditSaving == null)
                    {
                        return HttpNotFound();
                    }
                    saving.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
                    return View(saving);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        #endregion

        #region Création d'un compte pour un client => Get : /Account/{id}/Create/{typeCompte}
        public ActionResult Create(int? id, string typecompte)
        {
            ViewBag.IdClient = id;
            ViewBag.typecompte = typecompte;
            ViewBag.Url = System.Web.HttpContext.Current.Request.UrlReferrer;
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
                TempData.Add("Create", "Success");
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
            if (ModelState.IsValid)
            {
                deposit.Client = db.Clients.Find(id);
                db.Deposits.Add(deposit);
                db.SaveChanges();
                TempData.Add("Create", "Success");
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
                    editAccount.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
                    return View(editAccount);
                //break;
                case "Deposit":
                    editAccount.EditDeposit = db.Deposits.Find(idcompte);
                    editAccount.PersonID = id;
                    editAccount.TypeCompte = typecompte;
                    editAccount.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
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
        public ActionResult EditSaving(EditAccountViewModel account, int? id)/*[Bind(Include = "AccountID,BankCode,BranchCode,AccountNumber,Key,IBAN,BIC,Balance," +
            "MinimumAmount,MaximumAmount,InterestRate,MaximumDate")]*/
        {
            if (ModelState.IsValid)
            {
                db.Entry(account.EditSaving).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.Edit = "Success";
                TempData.Add("Edit", "Success");
                return RedirectToAction("Index", "Accounts", new { id = id});
            }
            return View("Edit", account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDeposit(EditAccountViewModel account, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account.EditDeposit).State = EntityState.Modified;
                db.SaveChanges();
                TempData.Add("Edit", "Success");
                return RedirectToAction("Index", "Accounts", new { id = id});
            }
            return View("Edit", account);
        }
        #endregion

        #region Suppression d'un compte client => Post : /Account/Delete/{id}/{typecompte}
        [HttpPost]
        public ActionResult Delete(int? id, string typecompte)
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
            if (typecompte == "Saving")
            {
                return PartialView("DeleteSaving",account);
            }
            else
            {
                return PartialView("DeleteDeposit", account);
            }
        }

        #endregion

        #region Suppression confirmée par l'user d'un compte Saving => Post : /Account/DeleteSaving/{id}
        [HttpPost, ActionName("DeleteSaving")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedSaving(int id)
        {
            Account account = db.Accounts.Find(id);
            ViewBag.IdClient = account.Client.PersonId;
            db.Accounts.Remove(account);
            db.SaveChanges();
            TempData.Add("Delete", "Success");
            return RedirectToAction("Index", new { id = ViewBag.IdClient });
        }

        #endregion

        #region Suppression confirmée par l'user d'un compte Deposit => Post : /Account/DeleteDeposit/{id}
        [HttpPost, ActionName("DeleteDeposit")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedDeposit(int id)
        {
            Deposit account = db.Deposits.Include("Cards").First(d => d.AccountID == id);
            ViewBag.IdClient = account.Client.PersonId;
            db.Deposits.Remove(account);
            db.SaveChanges();
            TempData.Add("Delete", "Success");
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
