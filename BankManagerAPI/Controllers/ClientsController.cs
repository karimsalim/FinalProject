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

        // GET: api/Clients/GetClientAmount
        public int GetClientAmount()
        {
            return db.Clients.Count();
        }

        // GET: api/Clients/GetClientAmount/{id}
        public int GetClientAmount(int id)
        {
            return db.Clients.Include("Employee").Where(e => e.Conseiller.Manager.PersonId == id).Count();
        }

        // GET: api/Clients/GetCardsPercentages
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
        // GET: api/Clients/GetCardsPercentages/{id}
        public double GetCardsPercentages(int id)
        {
            double nbClients = db.Clients.Include("Employee").Where(e => e.Conseiller.Manager.PersonId == id).Count();
            double nbClientsCarded = 0;
            bool isCounted = false;
            double percentage;
            if (nbClients == 0) { return 0; }
            List<Client> clientsCarded = new List<Client>();
            foreach (Deposit deposit in db.Deposits.Include("Cards").Include("Client").Where (e => e.Client.Conseiller.Manager.PersonId == id))
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

        //GET: api/Clients/GetSavingsPercentages
        public double GetSavingsPercentages()
        {
            double nbClients = db.Clients.Count();
            double nbClientsSavers = 0;
            bool isCounted = false;
            double percentage;
            List<Client> clientsSaved = new List<Client>();
            foreach (Savings saving in db.Savings.Include("Client"))
            {
                isCounted = false;
                foreach (Client client in clientsSaved)
                {
                    if (client.PersonId == saving.Client.PersonId)
                    {
                        isCounted = true;
                        break;
                    }
                }
                if (isCounted == false)
                {
                    clientsSaved.Add(saving.Client);
                }
            }
            nbClientsSavers = clientsSaved.Count();
            percentage = nbClientsSavers / nbClients * 100;
            percentage = Math.Round(percentage, 2);

            return percentage;
        }

        //GET: api/Clients/GetSavingsPercentages/{id}
        public double GetSavingsPercentages(int id)
        {
            double nbClients = db.Clients.Include("Employee").Where(e => e.Conseiller.Manager.PersonId == id).Count();
            double nbClientsSavers = 0;
            bool isCounted = false;
            double percentage;
            if (nbClients == 0) { return 0; }
            List<Client> clientsSaved = new List<Client>();
            foreach (Savings saving in db.Savings.Include("Client").Where(e => e.Client.Conseiller.Manager.PersonId == id))
            {
                isCounted = false;
                foreach (Client client in clientsSaved)
                {
                    if (client.PersonId == saving.Client.PersonId)
                    {
                        isCounted = true;
                        break;
                    }
                }
                if (isCounted == false)
                {
                    clientsSaved.Add(saving.Client);
                }
            }
            nbClientsSavers = clientsSaved.Count();
            percentage = nbClientsSavers / nbClients * 100;
            percentage = Math.Round(percentage, 2);

            return percentage;
        }

        //Get api/clients/GetClientList/{id}
        public IQueryable<Client> GetClients(int id)
        {
            return db.Clients.Include("Employee").Where(e => e.Conseiller.Manager.PersonId == id);
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