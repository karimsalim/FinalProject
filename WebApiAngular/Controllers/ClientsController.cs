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
using DomainModel;
using DAL;

namespace WebApiAngular.Controllers
{
    #region Class AccountClient
    public class AccountClient
    {
        public string[] Client { get; set; }
        public Employee Conseiller { get; set; }
        public List<Account> Deposit { get; set; }
        public List<Account> Saving { get; set; }

        public AccountClient(Client client)
        {
            //Récupère les informations du client afin de ne pas avoir le Conseiller et le Account du client
            this.Client = new string[7];
            #region tabClient
            this.Client[0] = client.PersonId.ToString();
            this.Client[1] = client.FirstName;
            this.Client[2] = client.LastName;
            this.Client[3] = client.DateOfBirth.ToString();
            this.Client[4] = client.Street;
            this.Client[5] = client.ZipCode;
            this.Client[6] = client.City;
            #endregion

            //Récupère conseiller du client
            Conseiller = client.Conseiller;

            //Récupère les comptes du client et les trient par rapport à leurs type de compte
            this.Deposit = new List<Account>();
            this.Saving = new List<Account>();

            //Sépare les type d'account pour la sérialisation pour Angular
            foreach (var account in client.Accounts)
            {
                if (account is Deposit)
                {
                    this.Deposit.Add(account);
                }
                else
                {
                    this.Saving.Add(account);
                }
            }
        }
    }
    #endregion
    public class ClientsController : ApiController
    {

        private readonly BankContext db = new BankContext();

        // GET: api/Clients
        public IQueryable<Client> GetPeople()
        {
            return db.Clients;

        }


        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetClient(string firstName, string lastName)
        {
            AccountClient client = new AccountClient(await db.Clients.SingleOrDefaultAsync(c => c.FirstName == firstName && c.LastName == lastName));
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.PersonId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransfert(int id, string rib, decimal montantOperation)
        {
            string[] tab = rib.Split("-".ToCharArray());
            string bankCode = tab[0];
            string branchCode = tab[1];
            string accountNumber = tab[2];
            string key = tab[3];

            Account accountDebiteur = await db.Accounts.FindAsync(id); // Compte du débiteur
            //Recherche le compte du par rapport au rib
            Account accountCrediteur = db.Accounts.SingleOrDefault(c => c.BankCode == bankCode && c.BranchCode == branchCode && c.AccountNumber == accountNumber && c.Key == key);
            if(accountCrediteur is null)
            {
                return BadRequest("Le compte créditeur n'existe pas");
            }

            #region Virement interne
            // ************* VIREMENT INTERNE *************** //
            if (accountCrediteur.Client.PersonId == accountDebiteur.Client.PersonId) 
            {
                #region Deposit to Deposite
                //***************  DEPOSIT TO DEPOSIT  ****************//
                if (accountDebiteur is Deposit && accountCrediteur is Deposit) 
                {
                    Deposit depositAccountDebiteur = await db.Deposits.FindAsync(accountDebiteur.AccountID);
                    Deposit depositAccountCrediteur = await db.Deposits.FindAsync(accountCrediteur.AccountID);

                    depositAccountDebiteur.Balance -= montantOperation;
                    //Si le solde du débiteur est supérieur au plafond de découvert : Valide
                    if(depositAccountDebiteur.Balance >= depositAccountDebiteur.AutorizedOverdraft) 
                    {
                        depositAccountCrediteur.Balance += montantOperation;
                        db.Entry(depositAccountCrediteur).State = EntityState.Modified;
                        db.Entry(depositAccountDebiteur).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        //Le solde du débiteur dépace le plafond autorisé
                        return BadRequest("Le solde du débiteur dépace le plafond de découvert autorisé"); //NON
                    }
                }
                #endregion

                #region Deposit to Saving
                //************** DEPOSIT TO SAVING ****************//
                else if (accountDebiteur is Deposit && accountCrediteur is Savings)
                {
                    Deposit depositAccountDebiteur = await db.Deposits.FindAsync(accountDebiteur.AccountID);
                    Savings depositAccountCrediteur = await db.Savings.FindAsync(accountCrediteur.AccountID);
                    depositAccountDebiteur.Balance -= montantOperation;
                    //Si le solde du débiteur est supérieur au plafond de découvert : Valide
                    if (depositAccountDebiteur.Balance >= depositAccountDebiteur.AutorizedOverdraft) 
                    {
                        depositAccountCrediteur.Balance += montantOperation;
                        //Si le solde du Créditeur est inférieur au montant maximum autorisé sur son compte épargne : Valide
                        if(depositAccountCrediteur.Balance <= depositAccountCrediteur.MaximumAmount)
                        {
                            db.Entry(depositAccountCrediteur).State = EntityState.Modified;
                            db.Entry(depositAccountDebiteur).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                             

                        } else {
                            //Le plafond du compte courant est dépacé
                            return BadRequest("Le solde du compte épargne du créditeur dépace le plafond maximal autorisé");
                        }
                    } else {
                        //Erreur le découvert du compte courant est dépacé
                        return BadRequest("Le solde du débiteur dépace le plafond autorisé");
                    }
                }
                #endregion

                #region Saving to Deposit
                // ************* SAVING TO DEPOSIT *************** //
                else if (accountDebiteur is Savings && accountCrediteur is Deposit) 
                {
                    Savings depositAccountDebiteur = await db.Savings.FindAsync(accountDebiteur.AccountID);
                    Deposit depositAccountCrediteur = await db.Deposits.FindAsync(accountCrediteur.AccountID);

                    depositAccountDebiteur.Balance -= montantOperation;

                    //Si le solde du épargne du débiteur reste supérieur au montant minimum du même compte : Valide
                    if (depositAccountDebiteur.Balance >= depositAccountDebiteur.MinimumAmount)
                    {
                        db.Entry(depositAccountCrediteur).State = EntityState.Modified;
                        db.Entry(depositAccountDebiteur).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    } else {
                        //Le Montant minimum du compte épargne est dépacé
                        return BadRequest("Le solde du compte épargne du débiteur dépace le plafond minimal autorisé");
                    }
                }
                #endregion

                #region Saving to Saving
                // *************** SAVING TO SAVING ************** //
                else if (accountDebiteur is Savings && accountCrediteur is Savings)
                {
                    Savings depositAccountDebiteur = await db.Savings.FindAsync(accountDebiteur.AccountID);
                    Savings depositAccountCrediteur = await db.Savings.FindAsync(accountCrediteur.AccountID);
                    depositAccountDebiteur.Balance -= montantOperation;

                    //Si le solde du débiteur est supérieur au plafond minimum : Valide
                    if(depositAccountDebiteur.Balance >= depositAccountDebiteur.MinimumAmount)
                    {
                        depositAccountCrediteur.Balance += montantOperation;

                        //Si le solde du créditeur est inférieur au plafond maximum : Valide
                        if(depositAccountCrediteur.Balance <= depositAccountCrediteur.MaximumAmount)
                        {
                            db.Entry(depositAccountCrediteur).State = EntityState.Modified;
                            db.Entry(depositAccountDebiteur).State = EntityState.Modified;
                            await db.SaveChangesAsync();

                        } else {
                            //Le plafond du compte courant est dépacé
                            return BadRequest("Le solde du compte épargne du créditeur dépace le plafond maximal autorisé");
                        }

                    } else {
                        //Le Montant minimum du compte épargne est dépacé
                        return BadRequest("Le solde du compte épargne du débiteur dépace le plafond minimal autorisé");
                    }
                }
                #endregion
            }
            #endregion

            #region Virement externe
            else
            {
                if(accountDebiteur is Deposit && accountCrediteur is Deposit)
                {
                    Deposit depositAccountDebiteur = await db.Deposits.FindAsync(accountDebiteur.AccountID);
                    Deposit depositAccountCrediteur = await db.Deposits.FindAsync(accountCrediteur.AccountID);
                    depositAccountDebiteur.Balance -= montantOperation;

                    //Si le solde du compte du débiteur est supérieur au plafond max autorisé
                    if (depositAccountDebiteur.Balance >= depositAccountDebiteur.AutorizedOverdraft)
                    {
                        depositAccountCrediteur.Balance += montantOperation;
                        db.Entry(depositAccountDebiteur).State = EntityState.Modified;
                        db.Entry(depositAccountCrediteur).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                    } else {
                        //Le solde du débiteur dépace le plafond autorisé
                        return BadRequest("Le solde du débiteur dépace le plafond de découvert autorisé"); //NON
                    }

                } else {
                    // L'un des compte est un saving
                    return BadRequest("Impossible de faire des virements via des compte épargnes");
                }
                
            }
            #endregion
            return StatusCode(HttpStatusCode.OK);
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