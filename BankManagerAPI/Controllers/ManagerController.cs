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
    public class ManagerController : ApiController
    {
        private BankContext db = new BankContext();

        // GET: api/Manager
        //public IQueryable<Employee> GetManager()
        public List<Employee> GetManager()
        {
            
            IQueryable<Employee> employees = db.Employees;
            List<Employee> managers = new List<Employee>();
            bool isManager = false;

            foreach (Employee employee in employees)
            {
                isManager = false;
                foreach (Employee employee2 in employees)
                {
                    if (employee2.Manager == employee)
                    {
                        isManager = true;
                        break;
                    }
                }
                if (isManager == true)
                {
                    managers.Add(employee);
                }
            }


            return managers;
        }

        // GET: api/Manager/5
        //[ResponseType(typeof(Employee))]
        //public async Task<IHttpActionResult> GetEmployee(int id)
        //{
        //    Employee employee = await db.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(employee);
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.People.Count(e => e.PersonId == id) > 0;
        }
    }
}