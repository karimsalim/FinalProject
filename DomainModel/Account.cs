using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public abstract class Account
    {
        #region Proprietes
        [Key]
        protected int AccountID { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "Code de Banque Requis.")]
        protected string BankCode { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "Code de Branche Requis.")]
        protected string BranchCode { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "Numéro de Compte Requis.")]
        protected string AccountNumber { get; set; }

        [StringLength(2)]
        [Required(ErrorMessage = "Clé Requise.")]
        protected string Key { get; set; }

        [StringLength(34)]
        [Required(ErrorMessage = "IBAN Requis.")]
        protected string IBAN { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "BIC Requis.")]
        protected string BIC { get; set; }

        [Required(ErrorMessage = "Solde de compte manquant.")]
        protected decimal Balance { get; set; }
        #endregion

        #region Relations
        protected virtual Client Client { get; set; }

        #endregion



    }
}
