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
    public class Client : Person
     {
        #region Propriété

        /// <summary>
        /// Rue de l'adresse du client
        /// </summary>
        [Required(ErrorMessage ="Veuillez saisir la rue ")]
        [DisplayName("Rue")]
        public string Street { get; set; }

        /// <summary>
        /// Code postal de l'adresse du client
        /// </summary>
        [Required(ErrorMessage ="Veuillez saisir le code postal de l'adresse")]
        [DisplayName("Code postal")]
        public string ZipCode { get; set; }
          
        /// <summary>
        /// Ville de l'adresse du client
        /// </summary>
        [Required(ErrorMessage ="Veuillez saisir la Ville ")]
        [DisplayName("Ville")]
        public string City { get; set; }
        #endregion

        #region Relation
        //propriétés commun avec class person 
        [JsonIgnore]
        public virtual ICollection<Account> Accounts { get; set; }
        [JsonIgnore]
        public virtual Employee Conseiller { get; set; }
        #endregion

    }

}
