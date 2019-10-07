using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInitBase
{
    class Program
    {
        static void Main(string[] args)
        {
            using(BankContext context = new BankContext())
            {
                Console.WriteLine("Création de la base en cours....");
                List<Employee> employees = context.Employees.ToList();
                Console.WriteLine("Création de la base réussite");
                Console.ReadLine();
            }
            
        }
    }
}
