using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackEndASP.ViewModels;
using DAL;

namespace BackEndASP.ViewModels
{
    public class EditAccountViewModel
    {

        public Savings EditSaving { get; set; }

        public Deposit EditDeposit { get; set; }

        public int? PersonID { get; set; }

        public string TypeCompte { get; set; }

    }
}