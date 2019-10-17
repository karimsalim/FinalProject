using System;
using System.Collections;
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

            #region Init données des employees
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

            #region Init données des clients 
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
                    LastName="Nawali",
                    DateOfBirth=DateTime.Parse("1975/05/07"),
                    City="Lyon",
                    ZipCode="69100",
                    Street="Rue Emile Zola",
                },
            };
            #endregion

            #region Init données des deposits
            List<Deposit> deposits = new List<Deposit>()
            {

                new Deposit()
                {
                   CreationDate=DateTime.Parse("2018/05/12"),
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
                   CreationDate=DateTime.Parse("2019/01/10"),
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
                   CreationDate=DateTime.Parse("2019/03/25"),
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
                   CreationDate=DateTime.Parse("2019/03/25"),
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

            #region Init données des Savings
            List<Savings> savings = new List<Savings>()
            {
                new Savings()
                {
                  
                   BankCode ="18591",
                   BranchCode="17274",
                   AccountNumber="12464164940",
                   Key="45",
                   BIC="IWMRPUCXAZ",
                   IBAN="RFP56954TAK",
                   Balance= 10000,
                    MinimumAmount=12,
                    MaximumAmount=200,
                    InterestRate=10,
                    MaximumDate=DateTime.Parse("2020/01/01"),
                },
                new Savings()
                {
                   BankCode ="18592",
                   BranchCode="17275",
                   AccountNumber="12464164947",
                   Key="44",
                   BIC="IWMRPUKXOY",
                   IBAN="RFP56954PXK",
                   Balance= 10000,
                    MinimumAmount=20,
                    MaximumAmount=5000,
                    InterestRate=10,
                    MaximumDate=DateTime.Parse("2020/05/05"),
                },
                new Savings()
                {
                   BankCode ="18590",
                   BranchCode="17270",
                   AccountNumber="12464164930",
                   Key="40",
                   BIC="KNMRPUCXOI",
                   IBAN="RFPO6954TUK",
                   Balance= 10000,
                    MinimumAmount=100,
                    MaximumAmount=10000,
                    InterestRate=10,
                    MaximumDate=DateTime.Parse("2024/04/15"),
                },
            };
            #endregion

            #region Init données de cartes bancaires
            List<Card> cards = new List<Card>()
            {
                new Card()
                {
                    NewtorkIssuer="VisaCard",
                    CardNumber="1993178516874882",
                    SecurityCode="5138",
                    ExpirationDate=DateTime.Parse("2020/01/12"),
                },

                new Card()
                {
                    NewtorkIssuer="AmexCard",
                    CardNumber="1302564804382137",
                    SecurityCode="6871",
                    ExpirationDate=DateTime.Parse("2020/01/02"),
                },
            };
            #endregion

            #region Init Listes des données
            List<Account> listcompte = new List<Account>();
            List<Account> listcompte2 = new List<Account>();
            List<Account> listcompte3 = new List<Account>();

            List<Client> listclient1 = new List<Client>();
            List<Client> listclient2 = new List<Client>();

            List<Card> listcards1 = new List<Card>();
            List<Card> listcards2 = new List<Card>();
            #endregion

            #region Ajout des clients pour l'employee

            listclient1.Add(clients[0]);
            listclient1.Add(clients[1]);

            listclient2.Add(clients[2]);
            

            employees[0].Clients = listclient1;
            employees[1].Clients = listclient2;

            #endregion

            #region Ajout des comptes pour les clients

            listcompte.Add(deposits[0]);
            listcompte.Add(deposits[1]);
            listcompte.Add(savings[0]);

            clients[0].Accounts = listcompte;

            listcompte2.Clear();
            listcompte2.Add(deposits[2]);
            listcompte2.Add(savings[1]);
            clients[1].Accounts = listcompte2;

            listcompte3.Clear();
            listcompte3.Add(deposits[3]);
            listcompte3.Add(savings[2]);

            clients[2].Accounts = listcompte3;

            #endregion

            #region Ajout des CB pour les comptes
            listcards1.Add(cards[0]);
            listcards2.Add(cards[1]);

            deposits[0].Cards = listcards1;
            deposits[3].Cards = listcards2;

            #endregion

            #region Ajout des managers pour les employees
            employees[0].Manager = employees[2];
            employees[1].Manager = employees[3];
            employees[2].Manager = employees[3];
            employees[3].Manager = employees[3];
            #endregion

            

            #region Ajout des données dans le context

            employees.ForEach(e => context.Employees.Add(e)); //Ajout de la liste des employes dans le context
            clients.ForEach(c => context.Clients.Add(c)); //Ajout de la liste des clients dans le context
            deposits.ForEach(d => context.Deposits.Add(d)); // Ajout de la liste des deposits dans le context
            savings.ForEach(s => context.Savings.Add(s)); //Ajout de la liste des savings dans le context

            #endregion

            #region Envoi des données à la BDD
            context.SaveChanges(); //Sauvegarde des données au coté de la BDD
            base.Seed(context);
            #endregion
        }

    }
}
