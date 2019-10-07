using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Savings : Account
    {
        [Required(ErrorMessage = "Une valeur minimale est requise.")]
        protected int MinimumAmount { get; set; }

        protected int MaximumAmount { get; set; }

        [Required(ErrorMessage = "Le taux d'intêret doit être précisé.")]
        protected double InterestRate { get; set; }

        protected DateTime MaximumDate { get; set; }

        protected virtual Account Account { get; set; }
    }
}
