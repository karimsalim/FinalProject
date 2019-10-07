using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        #region Proprietes
        [Required(ErrorMessage = "Veuillez saisir le type de carte bancaire")]
        public string NewtorkIssuer { get; set; }

        [Required(ErrorMessage = "Veuille saisir le N° de carte bancaire")]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le code confidentiel")]
        [StringLength(4)]
        public string SecurityCode { get; set; }

        [Required(ErrorMessage = "Veuillez saisir la date d'expiration")]
        public DateTime ExpirationDate { get; set; }
        #endregion

        #region Relations
        public Deposit Deposit { get; set; }
        #endregion


    }
}
