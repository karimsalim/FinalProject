using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DomainModel.Validators;

namespace DAL
{
    public class Savings : Account
    {
        #region Propriété

        /// <summary>
        /// Montant minimal du compte
        /// </summary>
        [Required(ErrorMessage = "Le montant minimum du compte est requis.")]
        [DisplayName("Montant minimum")]
        [DataType(DataType.Currency)]
        public int MinimumAmount { get; set; }

        /// <summary>
        /// Montant maximum autorisé. Peut être null.
        /// </summary>
        [DisplayName("Montant maximum")]
        [DataType(DataType.Currency)]
        public int? MaximumAmount { get; set; }

        /// <summary>
        /// Taux d'intéret du compte
        /// </summary>
        [DisplayName("Taux d'intéret")]
        [DisplayFormat(DataFormatString = "{0} %")]
        [Required(ErrorMessage = "Le taux d'intêret doit être précisé.")]
        public double InterestRate { get; set; }

        /// <summary>
        /// Date maximum d'existence du compte. Peut être null.
        /// </summary>
        [DisplayName("Date d'utilisation limitée du compte")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DateValueValidator]
        public DateTime? MaximumDate { get; set; }
        #endregion
    }

    

}
