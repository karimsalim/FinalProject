using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace BankManagerAPI.Controllers
{
    public class AccountsController : ApiController
    {
        private BankContext db = new BankContext();

        // GET: api/Accounts/GetTotalBalance
        public decimal GetTotalBalance()
        {
            return db.Accounts.Sum(e => e.Balance);
        }



        //GET api/Accounts/GetSavingSum
        public decimal GetSavingSum()
        {
            return db.Savings.Sum(e => e.Balance);
        }

        //GET api/Accounts/GetSavingSum/{id}
        public decimal GetSavingSum(int id)
        {
            return db.Savings.Include("Person").Where(e => e.Client.Conseiller.Manager.PersonId == id).Sum(e => e.Balance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountExists(int id)
        {
            return db.Accounts.Count(e => e.AccountID == id) > 0;
        }
    }
}