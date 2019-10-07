using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Client : Person

    {       /*les propriétés clients */
        [Required(ErrorMessage ="Veuillez saisir la rue ")]
        protected string Street { get; set; }

        [Required(ErrorMessage ="Veuillez saisir votre ZipCode")]
        protected string ZipCode { get; set; }
          
        [Required(ErrorMessage ="Veuillez saisir la Ville ")]
        protected string City { get; set; }
       
        //propriétés commun avec class person 
        protected virtual ICollection<Account> Accounts { get; set; }
        protected virtual Employee Conseiller { get; set; }
        
    }

}
