using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Employee : Person
    {
        [Required(ErrorMessage = "Veuillez saisir le nom du bureau")]
        protected string OfficeName { get; set; }

        [Required]
        protected bool isJunior { get; set; }


        protected virtual List<Client> Clients { get; set; }
        protected virtual Employee Manager { get; set; }

    }
}
