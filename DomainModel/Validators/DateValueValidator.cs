using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Validators
{
    /// <summary>
    /// Classe pour la validation de la date maximum d'un Saving
    /// </summary>
    internal class DateValueValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /* SI LA DATE SAISIE EST NULL => VALIDE DANS LE CONTEXT, ON AJOUTE 99 ANS A LA DATE D'AUJOURD'HUI POUR VALIDER LA PROPRIETE
             * SINON ERREUR DE SAISIE */
            DateTime dates = value == null ? DateTime.Now.AddYears(99) : DateTime.Parse(value.ToString());
            if (dates < DateTime.Now)
            {
                return new ValidationResult("La date sélectionnée ne doit pas être inférière à la date d'aujourd'hui.");
            }
            return ValidationResult.Success;
        }
    }
}
