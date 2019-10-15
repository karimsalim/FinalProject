using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Card
    {
        #region Proprietes
        [Key]
        public int CardId { get; set; }


        /// <summary>
        /// Type de carte bancaire
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir le type de carte bancaire")]
        [DisplayName("Type de carte bancaire")]
        [RegularExpression(@"[a-zA-Z0-9]*", ErrorMessage ="Veuillez saisir le type de carte bancaire")]
        public string NewtorkIssuer { get; set; }


        /// <summary>
        /// Numéro de carte bancaire
        /// </summary>
        [Required(ErrorMessage = "Veuille saisir le N° de carte bancaire")]
        [StringLength(16)]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage="Le N° de carte bancaire doit être composé de 16 chiffres")]
        [DisplayName("Numéro de carte bancaire")]
        public string CardNumber { get; set; }


        /// <summary>
        /// Code confidentiel
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir le code confidentiel")]
        [StringLength(4)]
        [DisplayName("Code confidentiel")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage="Le code confidentiel doit être composé de 4 chiffres")]
        public string SecurityCode { get; set; }


        /// <summary>
        /// Date d'expiration
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir la date d'expiration")]
        [DisplayName("Date d'expiration")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        #endregion

        #region Relations
        public Deposit Deposit { get; set; }
        #endregion


    }
}
