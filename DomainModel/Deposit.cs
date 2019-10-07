using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Deposit : Account
    {
        #region Proprietes
        [Required(ErrorMessage ="Veuillez saisir la date de gestion"]
        protected DateTime GestionDate { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le montant autorisé")]
        protected decimal AutorizedOverdraft { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le montant")]
        protected decimal FreeOverdraft { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le montant de taux")]
        protected double OverdraftChargeRate { get; set; }
        #endregion

        #region Relations
        protected virtual Account CompteBancaire { get; set; }

        protected virtual List<Card> Card { get; set; }
        #endregion

    }
}
