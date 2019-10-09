using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using BackEndASP.ViewModels;

namespace BackEndASP.ViewModels
{
    /// <summary>
    /// Classe pour l'assemblage des différents type de comptes
    /// </summary>
    public class AccountViewModel
    {
        public List<Savings> Savings { get; set; }

        public List<Deposit> Deposits { get; set; }

        public int? PersonID { get; set; }
    }
}