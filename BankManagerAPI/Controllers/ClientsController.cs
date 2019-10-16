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

        public int GetClientManaged(int id)
        {
            return db.Clients.Include("Employee").Where(e => e.Conseiller.Manager.PersonId == id).Count();
        }

        public double GetCardsPercentages()
        {
            double nbClients = db.Clients.Count();
            double nbClientsCarded = 0;
            bool isCounted = false;
            double percentage;
            List<Client> clientsCarded = new List<Client>();
            foreach (Deposit deposit in db.Deposits.Include("Cards").Include("Client"))
            {
                if (deposit.Cards.Count() != 0)
                {
                    isCounted = false;
                    foreach (Client client in clientsCarded)
                    {
                        if (client.PersonId == deposit.Client.PersonId)
                        {
                            isCounted = true;
                            break;
                        }
                    }
                    if (isCounted == false)
                    {
                        clientsCarded.Add(deposit.Client);
                    }
                }
            }
            nbClientsCarded = clientsCarded.Count();
            percentage = nbClientsCarded / nbClients * 100;
            percentage = Math.Round(percentage, 2);

            return percentage;
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