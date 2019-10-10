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
        public ActionResult listEmployeDebug()
        {
            var listEmployee = db.Employees.ToList();
            return View(listEmployee);
        }


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
            //
            ViewBag.IdEmployee = employee.PersonId; //id nécessaire pour le paramètre de création d'employé et client ainsi que leurs listes.
            return View();
        }

        public ActionResult ListEmployee(int? id)
        {
            Employee employee = db.Employees.Find(id); //Récupère l'employé de l'id courante
            var listEmployee = db.Employees.ToList(); //Génération de la liste des employés
            ViewBag.IdManagerForBackToList = employee.PersonId; //Transport l'id du manager pour le Back de la ViewListEmploye
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

            //Si le nom du manager est différent de l'employé courant
            if (employee.FirstName != employee.Manager.FirstName)
            {
                ViewBag.Manager = employee.Manager.FirstName; //Génération de la liste des employés
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

            EmployeeViewModel employeeViewModel = new EmployeeViewModel(); //Créer un objet ViewModel aillant un objet Employee et une SelectList
            employeeViewModel.CurrentEmployee = employee;

            //Objet listeEmployee, value PersonId, Texte affiché LastName, séléctionne par defaut le manager de l'employé
            employeeViewModel.ListManager = new SelectList(listEmployee, "PersonId", "LastName", employee.Manager?.PersonId);

            ViewBag.IdManagerForBackToList = employee.Manager.PersonId; //Transport l'id du manager pour le Back de la ViewDetail

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
        public ActionResult Edit([Bind(Exclude = "ListManager")] EmployeeViewModel employeeViewModel)
        {
            Employee employee = db.Employees.Find(employeeViewModel.CurrentEmployee.PersonId); //Recherche l'employé que l'on modifie
            int currentManger = employee.Manager.PersonId;//Recupère l'id du manager travaillant sur l'employé afin de retourner sur la liste des employé de celui-ci
            Employee manager = db.Employees.Find(employeeViewModel.CurrentEmployee.Manager.PersonId); //Recherche le manager séléctionner dans la combobox
            employee.Manager = manager; //Assigne le manager à l'employé

            //if (ModelState.IsValid)
            //{
            db.Entry(employee).CurrentValues.SetValues(employeeViewModel.CurrentEmployee); //Récupère la valeur courrante et la remplace par la nouvelle
            db.SaveChanges();
            return RedirectToAction("ListEmployee/" + currentManger);
            //}
            //return View(employeeViewModel);
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
            int currentManger = employee.Manager.PersonId; //Récupère l'id du manager afin de retourner sur la liste des employé de celui-ci
            db.People.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("ListEmployee/" + currentManger);
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
