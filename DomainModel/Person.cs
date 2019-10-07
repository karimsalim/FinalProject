using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un nom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un prénom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Veuillez saisir une date de naissance")]
        public DateTime DateOfBirth { get; set; }
    }
}
