using System/
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class BankInitializer : DropCreateDatabaseAlways<BankContext>
    {
        protected override void Seed(BankContext context)
        {
            #region clients 
            List<Client> clients = new List<Client>()
            {
                new Client(){
                    FirstName = "Etienne",
                    LastName ="Suhard",
                    DateOfBirth = DateTime.Parse("1995/12/01"),
                    City="Kumbuge",
                    ZipCode="321045",
                    Street ="Rue MiKael Jackson",

                },

                new Client()
                {
                    FirstName="Karen",
                    LastName="Bacon",
                    DateOfBirth=DateTime.Parse("1979/02/01"),
                    City="Mosco",
                    ZipCode="25487",
                    Street="Rue Rockefeller",
                },

                new Client()
                {
                    FirstName="Edith",
                    LastName="Layali",
                    DateOfBirth=DateTime.Parse("1975/05/07"),
                    City="Lyon",
                    ZipCode="69100",
                    Street="Rue Emile Zola",
                },
            };
            #endregion

            #region employees
            List<Employee> employees = new List<Employee>()
            {
               new Employee()
               {
                   FirstName="Fabrice",
                   LastName="Roussignole",
                   DateOfBirth=DateTime.Parse("1992/02/21"),
                   OfficeName="Etienne Bank",
                   isJunior =true,
               },

               new Employee()
               {
                   FirstName="Mourice",
                   LastName="Badran",
                   DateOfBirth=DateTime.Parse("1997/05/11"),
                   OfficeName="Etienne Bank",
                   isJunior =true,
               },
                new Employee()
               {
                   FirstName="Biky",
                   LastName="Boo",
                   DateOfBirth=DateTime.Parse("1975/08/11"),
                   OfficeName="Etienne Bank",
                   isJunior =false,
               },

                 new Employee()
               {
                   FirstName="Tom",
                   LastName="Tom",
                   DateOfBirth=DateTime.Parse("2001/10/21"),
                   OfficeName="Etienne Bank",
                   isJunior =true,
               },
            };
            #endregion

            #region deposits
            List<Deposit> deposits = new List<Deposit>()
            {

                new Deposit()
                {
                   GestionDate=DateTime.Parse("2018/05/12"),
                   BankCode ="37951",
                   BranchCode="16287",
                   Key="75",
                   BIC="FRTRPUCXER",
                   IBAN="RFT58954TIY",
                   Balance= 4500,
                   AutorizedOverdraft= -100,
                   FreeOverdraft=2,
                   AccountNumber="16464164972",
                   OverdraftChargeRate=20.6,

                },
                 new Deposit()
                {
                   GestionDate=DateTime.Parse("2019/01/10"),
                   BankCode ="37584",
                   BranchCode="16287",
                   Key="73",
                   BIC="KWMRPUCXER",
                   IBAN="RFP56954TIY",
                   Balance= 10000,
                   AutorizedOverdraft= -500,
                   FreeOverdraft=2,
                   AccountNumber="16464164973",
                   OverdraftChargeRate=20.6,

                },
                   new Deposit()
                {
                   GestionDate=DateTime.Parse("2019/03/25"),
                   BankCode ="37591",
                   BranchCode="16274",
                   Key="72",
                   BIC="IWMRPUCXOY",
                   IBAN="RFP56954TXK",
                   Balance= 10000,
                   AutorizedOverdraft= -500,
                   FreeOverdraft=2,
                   AccountNumber="16464164972",
                   OverdraftChargeRate=20.6,
                },

                     new Deposit()
                {
                   GestionDate=DateTime.Parse("2019/03/25"),
                   BankCode ="37591",
                   BranchCode="16274",
                   Key="72",
                   BIC="IWMRPUCXOY",
                   IBAN="RFP56954TXK",
                   Balance= 10000,
                   AutorizedOverdraft= -500,
                   FreeOverdraft=2,
                   AccountNumber="16464164972",
                   OverdraftChargeRate=20.6,
                },

            };
            #endregion

            #region Savings
            List<Savings> savings = new List<Savings>()
            {
                new Savings()
                {
                    MinimumAmount=12,
                    MaximumAmount=200,
                    InterestRate=10,
                    MaximumDate=DateTime.Parse("2 ans"),
                },

                new Savings()
                {
                    MinimumAmount=20,
                    MaximumAmount=5000,
                    InterestRate=10,
                    MaximumDate=DateTime.Parse("2 ans"),
                },
                  new Savings()
                {
                    MinimumAmount=100,
                    MaximumAmount=10000,
                    InterestRate=10,
                    MaximumDate=DateTime.Parse("3 ans"),
                },
            };
            #endregion
            base.Seed(context);
        }

    }
}
