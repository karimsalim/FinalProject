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
        #region Propriété
        [Required(ErrorMessage = "Veuillez saisir le nom du bureau")]
        public string OfficeName { get; set; }

        [Required]
        public bool isJunior { get; set; }
        #endregion

        #region Relation
        public virtual ICollection<Client> Clients { get; set; }
        public virtual Employee Manager { get; set; }
        #endregion

    }
}
