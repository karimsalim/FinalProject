using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using BackEndASP.ViewModel;

namespace BackEndASP.Controllers
{
    public class EmployeesController : Controller
    {
        private BankContext db = new BankContext();

        // GET: Employees
        public ActionResult Index(int? id)
        {
            Employee employee = db.Employees.Find(id); //Récupère l'employé de l'id courante
            var listEmployee = db.Employees.ToList(); //Génération de la liste des employés
            ViewBag.EmployeManager = employee.Manager.LastName; //Récupère le manager de l'employé courant

            //Parcours de la liste des employés
            foreach (var item in listEmployee)
            {
                //Si l'employé parcouru possède comme manager l'employé courant alors l'employé courant est un manager
                if (item.Manager.PersonId == employee.PersonId)
                {
                    ViewBag.isManager = true;
                }
            }
            ViewBag.IdEmployee = employee.PersonId;
            return View();
        }

        public ActionResult ListEmployee(int? id)
        {
            Employee employee = db.Employees.Find(id); //Récupère l'employé de l'id courante
            var listEmployee = db.Employees.ToList(); //Génération de la liste des employés
            List<Employee> list = null;

            //Parcours de la liste des employés
            foreach (var item in listEmployee)
            {
                //Si l'employé possède comme manager l'employé courant
                if (item.Manager.PersonId == employee.PersonId)
                {
                    //Si l'employé n'est pas l'employé courant
                    if (item.PersonId != employee.PersonId)
                    {
                        if (list is null)
                        {
                            list = db.Employees.Where(c => c.PersonId == item.PersonId).ToList(); // Si la liste est vide ajoute 1 élément
                        }
                        else
                        {
                            list.AddRange(db.Employees.Where(c => c.PersonId == item.PersonId).ToArray<Employee>()); // Sinon si possède déjà 1 élément ou plus rajoute 1 élément dans la liste
                        }

                    }
                }
            }
            return View(list);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id); //Récupère l'employé de l'id courante

            string manager = employee.Manager.FirstName; //Génération de la liste des employés



            //Si le nom du manager est différent de l'employé courant
            if (employee.FirstName != employee.Manager.FirstName)
            {
                ViewBag.Manager = manager;
                ViewBag.DisplayManager = "Manager :";
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,FirstName,LastName,DateOfBirth,OfficeName,isJunior")] Employee employee, int? id)
        {
            if (ModelState.IsValid)
            {
                Employee manager = db.Employees.Find(id); //Récupère l'employé de l'id courante

                //Gestion d'erreur, vérification de l'éxistance de l'employé
                if (manager is null)
                {
                    return HttpNotFound();
                }
                else
                {
                    employee.Manager = manager; //Le manager de l'employé crée est l'employé courant (celui qui le créer)

                    db.People.Add(employee);                               //
                    db.SaveChanges();                                     // Ajout et sauvegarde dans la bdd
                    return RedirectToAction("ListEmployee/" + id);       //
                }

            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var listEmployee = db.Employees.ToList(); //Récupère la liste des employé
            Employee employee = db.Employees.Find(id); //Récupère l'id de l'employé courant

            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.CurrentEmployee = employee;
            employeeViewModel.ListManager = new SelectList(listEmployee, "PersonId", "LastName", employee.Manager?.PersonId);

            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = EmployeeViewModel.CurrentManager.LastName, Value = "0", Selected = true });
            //items.Add(new SelectListItem { Text = EmployeeViewModel.ListEmployee, Value = "1" });

            //SelectList selectList = 


            if (employeeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(employeeViewModel);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,FirstName,LastName,DateOfBirth,OfficeName,isJunior")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.People.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
