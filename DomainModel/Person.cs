using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class Person
    {
        #region Propriété
        [Key]
        public int PersonId { get; set; }

        /// <summary>
        /// Prénom d'une personne
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir le prénom")]
        [DisplayName("Prénom")]
        public string FirstName { get; set; }


        /// <summary>
        /// Nom de famille d'une personne
        /// </summary>
        [DisplayName("Nom de famille")]
        [Required(ErrorMessage = "Veuillez saisir le nom de famille")]
        public string LastName { get; set; }


        /// <summary>
        /// Date de naissance
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir une date de naissance")]
        [DisplayName("Date de naissance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        #endregion
    }
}
