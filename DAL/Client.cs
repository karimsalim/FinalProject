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
        public string Street { get; set; }

        [Required(ErrorMessage ="Veuillez saisir votre ZipCode")]
        public string ZipCode { get; set; }
          
        [Required(ErrorMessage ="Veuillez saisir la Ville ")]
        public string City { get; set; }
       
        //propriétés commun avec class person 
        public virtual List<Account> Accounts { get; set; }
        public virtual Person Person { get; set; }
        
    }

}
