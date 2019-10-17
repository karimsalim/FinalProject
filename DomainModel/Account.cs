using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Newtonsoft.Json;

namespace DAL
{
    public class Account
    {
        #region Proprietes

        [Key]
        public int AccountID { get; set; }

        /// <summary>
        /// Code banque
        /// </summary>
        [StringLength(5)]
        [DisplayName("Code de banque")]
        [Required(ErrorMessage = "Code de Banque Requis.")]
        public string BankCode { get; set; }


        /// <summary>
        /// Code de branche
        /// </summary>
        [StringLength(5)]
        [DisplayName("Code de branche")]
        [Required(ErrorMessage = "Code de Branche Requis.")]
        public string BranchCode { get; set; }


        /// <summary>
        /// Numéro de compte
        /// </summary>
        [StringLength(11)]
        [DisplayName("Numéro de compte")]
        [Required(ErrorMessage = "Numéro de Compte Requis.")]
        public string AccountNumber { get; set; }


        /// <summary>
        /// Clé du compte
        /// </summary>
        [StringLength(2)]
        [DisplayName("Clé")]
        [Required(ErrorMessage = "Clé Requise.")]
        public string Key { get; set; }


        /// <summary>
        /// IBAN
        /// </summary>
        [StringLength(34)]
        [Required(ErrorMessage = "IBAN Requis.")]
        [DisplayName("IBAN")]
        public string IBAN { get; set; }


        /// <summary>
        /// BIC
        /// </summary>
        [StringLength(11)]
        [Required(ErrorMessage = "BIC Requis.")]
        [DisplayName("BIC")]
        public string BIC { get; set; }


        /// <summary>
        /// Solde de compte
        /// </summary>
        [Required(ErrorMessage = "Solde de compte manquant.")]
        [DisplayName("Solde de compte")]
        public decimal Balance { get; set; }
        #endregion

        #region Relations
        [JsonIgnore]
        public virtual Client Client { get; set; }
        #endregion

        #region Methodes
        /// <summary>
        /// Permet de récupérer le RIB (BBAN) d'un compte
        /// </summary>
        /// <returns>BBAN du compte sélectionné</returns>
        public string GetRib()
        {
            return $"{this.BankCode}-{this.BranchCode}-{this.AccountNumber}-{this.Key}"; 
        }
        #endregion


    }
}
