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
        #region Proprietes
        [Key]
        public int AccountID { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "Code de Banque Requis.")]
        public string BankCode { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "Code de Branche Requis.")]
        public string BranchCode { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "Numéro de Compte Requis.")]
        public string AccountNumber { get; set; }

        [StringLength(2)]
        [Required(ErrorMessage = "Clé Requise.")]
        public string Key { get; set; }

        [StringLength(34)]
        [Required(ErrorMessage = "IBAN Requis.")]
        public string IBAN { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "BIC Requis.")]
        public string BIC { get; set; }

        [Required(ErrorMessage = "Solde de compte manquant.")]
        public decimal Balance { get; set; }
        #endregion

        #region Relations
        public virtual Client Client { get; set; }

        #endregion



    }
}
