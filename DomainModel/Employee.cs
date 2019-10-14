using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; }
        [JsonIgnore]
        public virtual Employee Manager { get; set; }
        #endregion

    }
}
