using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BankContext : DbContext
    {

        #region Initialisation des DBSet => Entitées du SI
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Savings> Savings { get; set; }

        public DbSet<Person> People { get; set; }
        #endregion

        #region Constructeur
        public BankContext() : base("BankDb"){}
        #endregion

        #region Constructeur Statique
        /// <summary>
        /// Crée la BDD directement et supprime à chaque appel
        /// </summary>
        static BankContext()
        {
            //Database.SetInitializer(new BankInitializer());
        }
        #endregion

        #region Méthode OnModelCreating
        /// <summary>
        /// <para>Initialisation des tables de la BDD avec les relations.</para>
        /// <para>modelBuilder : Variable de type <see cref="DbModelBuilder"/> utilisé pour le mapping de la BDD.</para>
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Initialisation des tables du namespace CB
            modelBuilder.Entity<Account>().ToTable(schemaName: "CB", tableName: "Account");
            modelBuilder.Entity<Card>().ToTable(schemaName: "CB", tableName: "Cards");
            modelBuilder.Entity<Deposit>().ToTable(schemaName: "CB", tableName: "Deposits");
            modelBuilder.Entity<Savings>().ToTable(schemaName: "CB", tableName: "Savings");
            #endregion

            #region Initialisation des tables du namespace BCR
            modelBuilder.Entity<Employee>().ToTable(schemaName: "BCR", tableName: "Employes");
            modelBuilder.Entity<Client>().ToTable(schemaName: "BCR", tableName: "Clients");
            modelBuilder.Entity<Person>().ToTable(schemaName: "BCR", tableName: "Person");
            #endregion

            //Ajout Clef primaire dans la table Personne\\
            modelBuilder.Entity<Person>()
                .HasKey(P => P.PersonId)
                .Property(P => P.PersonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Ajout Clef primaire dans la table Card\\
            modelBuilder.Entity<Card>()
                .HasKey(C => C.CardId)
                .Property(C => C.CardId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            //Ajout Clef primaire dans la table Account\\
            modelBuilder.Entity<Account>()
                .HasKey(A => A.AccountID)
                .Property(A => A.AccountID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Modification du nom de la colonne du Conseiller coté Client\\
            //modelBuilder.Entity<Client>()
            //    .Property(C => C.Conseiller.PersonId)
            //    .HasColumnName("Conseiller");

            //Ajout Relation Entre Client et Account => 1 client a +eurs Comptes\\
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Accounts)
                .WithRequired(A => A.Client)
                .WillCascadeOnDelete(true);

            //Ajout Relation Entre Client et Employe => 1 employe a +eurs Clients\\
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Clients)
                .WithRequired(c => c.Conseiller)
                .WillCascadeOnDelete(false);

            //Ajout Relation Entre Deposit et Card => 1 Deposit possede +eurs Cards\\
            modelBuilder.Entity<Deposit>()
                .HasMany(D => D.Cards)
                .WithOptional(C => C.Deposit)
                .WillCascadeOnDelete(true);  // Si on supprime un compte, les cartes ne sont plus valides!

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
