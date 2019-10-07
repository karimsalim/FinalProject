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
        protected int PersonId { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un nom")]
        protected string FirstName { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un prénom")]
        protected string LastName { get; set; }

        [Required(ErrorMessage = "Veuillez saisir une date de naissance")]
        protected DateTime DateOfBirth { get; set; }
    }
}
