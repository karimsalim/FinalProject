using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TestInitBase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BankContext context = new BankContext())
            {
                //Console.WriteLine("Création de la base en cours....");
                //List<Employee> employees = context.Employees.ToList();
                //Console.WriteLine("Création de la base réussite");

                SHA256CryptoServiceProvider sHA = new SHA256CryptoServiceProvider();

                string motPasse = "MonMotDePasse";
                string motCrypte = "";

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Chaine en claire => {motPasse}");
                    motCrypte = BitConverter.ToString(sHA.ComputeHash(Encoding.UTF8.GetBytes(motPasse)));
                    Console.WriteLine($"Chaine hachée => {motCrypte}");
                    Console.WriteLine("---------------------------------");
                }
                Console.ReadLine();
            }

        }
    }
}
