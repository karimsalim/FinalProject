using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Validators
{
    internal class DateValueValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dates = value == null ? DateTime.Now.AddYears(99) : DateTime.Parse(value.ToString());
            if (dates < DateTime.Now)
            {
                return new ValidationResult("La date sélectionnée ne doit pas être inférière à la date d'aujourd'hui.");
            }
            return ValidationResult.Success;
        }
    }
}
