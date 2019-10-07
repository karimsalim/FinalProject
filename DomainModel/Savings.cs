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
        public int MinimumAmount { get; set; }

        public int MaximumAmount { get; set; }

        [Required(ErrorMessage = "Le taux d'intêret doit être précisé.")]
        public double InterestRate { get; set; }

        public DateTime MaximumDate { get; set; }
    }
}
