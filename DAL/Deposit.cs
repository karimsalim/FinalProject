using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Deposit
    {
        #region Proprietes
        [Required(ErrorMessage ="Veuillez saisir la date de gestion"]
        public DateTime GestionDate { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le montant autorisé")]
        public decimal AutorizedOverdraft { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le montant")]
        public decimal FreeOverdraft { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le montant de taux")]
        public double OverdraftChargeRate { get; set; }
        #endregion

        #region Relations
        public virtual Account CompteBancaire { get; set; }

        public virtual List<Card> Card { get; set; }
        #endregion

    }
}
