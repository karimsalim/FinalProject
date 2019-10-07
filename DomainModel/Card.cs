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

        #region Proprietes
        [Required(ErrorMessage = "Veuillez saisir le type de carte bancaire")]
        protected string NewtorkIssuer { get; set; }

        [Required(ErrorMessage = "Veuille saisir le N° de carte bancaire")]
        [StringLength(16)]
        protected string CardNumber { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le code confidentiel")]
        [StringLength(4)]
        protected string SecurityCode { get; set; }

        [Required(ErrorMessage = "Veuillez saisir la date d'expiration")]
        protected DateTime ExpirationDate { get; set; }
        #endregion

        #region Relations
        protected Deposit Deposit { get; set; }
        #endregion


    }
}
