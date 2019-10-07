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
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le nom du bureau")]
        public string OfficeName { get; set; }

        [Required]
        public bool isJunior { get; set; }


        public virtual List<Client> Clients { get; set; }
        public virtual Employee Manager { get; set; }

    }
}
