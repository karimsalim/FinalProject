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
        protected int MinimumAmount { get; set; }

        protected int MaximumAmount { get; set; }

        protected double InterestRate { get; set; }

        protected DateTime MaximumDate { get; set; }
    }
}
