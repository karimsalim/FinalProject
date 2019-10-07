using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Account
    {
        [Key]
        protected int AccountID { get; set; }

        [StringLength(5)]
        protected string BankCode { get; set; }

        [StringLength(5)]
        protected string BranchCode { get; set; }

        [StringLength(11)]
        protected string AccountNumber { get; set; }

        [StringLength(2)]
        protected string Key { get; set; }

        [StringLength(34)]
        protected string IBAN { get; set; }

        [StringLength(11)]
        protected string BIC { get; set; }

        protected decimal Balance { get; set; }

        virtual Client Client { get; set; }



    }
}
