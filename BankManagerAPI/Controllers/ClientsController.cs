using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace BankManagerAPI.Controllers
{
    public class ClientsController : ApiController
    {
        private BankContext db = new BankContext();

        // GET: api/Clients
        public int GetClientAmount()
        {
            return db.Clients.Count();
        }

        public int GetClientManaged(int personId)
        {
            return db.Clients.Include("Employee").Where(e => e.Conseiller.Manager.PersonId == personId).Count();
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.People.Count(e => e.PersonId == id) > 0;
        }
    }
}