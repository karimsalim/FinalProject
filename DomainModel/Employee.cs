using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Employee : Person
    {
        #region Propriété

        /// <summary>
        /// Nom du département
        /// </summary>
        [Required(ErrorMessage = "Veuillez saisir le nom du bureau")]
        [DisplayName("Nom du département")]
        public string OfficeName { get; set; }

        /// <summary>
        /// Niveau de l'employé
        /// </summary>
        [Required]
        [DisplayName("Niveau junior")]
        public bool isJunior { get; set; }
        #endregion

        #region Relation
        public virtual ICollection<Client> Clients { get; set; }
        public virtual Employee Manager { get; set; }
        #endregion

    }
}
