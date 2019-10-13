using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DAL
{
    public class Deposit : Account
    {
        #region Proprietes

        /// <summary>
        /// Date de création du compte
        /// </summary>
        [Required(ErrorMessage ="Veuillez saisir la date de création du compte")]
        [DisplayName("Crée le ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreationDate { get; set; }


        /// <summary>
        /// Montant autorisé
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir le montant autorisé")]
        [DisplayName("Découvert limité")]
        public decimal AutorizedOverdraft { get; set; }


        /// <summary>
        /// Découvert autorisé non taxé
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir le montant")]
        [DisplayName("Découvert autorisé")]
        public decimal FreeOverdraft { get; set; }


        /// <summary>
        /// Taux de taxe de découvert
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir le montant de taux")]
        [DisplayName("Taux de taxe")]
        public double OverdraftChargeRate { get; set; }
        #endregion

        #region Relations

        public virtual ICollection<Card> Cards { get; set; }
        #endregion

    }
}
