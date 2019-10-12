using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackEndASP.ViewModel;
using DAL;

namespace BackEndASP.Controllers
{
    public class ClientsController : Controller
    {
        private BankContext db = new BankContext();

        // GET: Clients
        public ActionResult Index(int? id)
        {
            ViewBag.idManager = id;
            List<Client> listClient = null;

            Employee employee = db.Employees.Find(id);
           
            if (listClient is null)
            {
                listClient = db.Clients.Where(c => c.Conseiller.PersonId== id).ToList();
            } else
            {
                listClient.AddRange(db.Clients.Where(c => c.Conseiller.PersonId == id).ToArray<Client>());
            }
          
            return View(listClient);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            ViewBag.idConseiller = client.Conseiller.PersonId;
            if (client == null)
            {
                return HttpNotFound();
            }

            // Affiche les details de comptes clients 
           
            ViewBag.Id = id;
            return View(client);
           
            
        }

        // GET: Clients/Create
        public ActionResult Create(int? id)
        {
            Employee manager = db.Employees.Find(id);
            ViewBag.IdEmployee = manager.PersonId;
            return View();
        }

        // POST: Clients/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,FirstName,LastName,DateOfBirth,Street,ZipCode,City")] Client client, int? id)
        {
            if (ModelState.IsValid)
            {
                Employee employee = db.Employees.Find(id);
                client.Conseiller = employee;
                db.People.Add(client);
                db.SaveChanges();
                return RedirectToAction($"{id}", "Employees");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);

            ClientViewModel clientViewModel = new ClientViewModel();
            clientViewModel.currentClient = client;
            clientViewModel.idConseiller = client.Conseiller.PersonId;

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(clientViewModel);
        }

        // POST: Clients/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel clientViewModel)
        {
            Employee conseiller = db.Employees.Find(clientViewModel.idConseiller);
            clientViewModel.currentClient.Conseiller = conseiller;

            if (ModelState.IsValid)
            {
                db.Entry(clientViewModel.currentClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/"+ conseiller.PersonId);
            }
            return View(clientViewModel.currentClient);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            ViewBag.idConseiller = client.Conseiller.PersonId;
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            int idConseiller = client.Conseiller.PersonId;
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index/" + idConseiller);
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





