using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BackEndASP.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Identifiant")]
        public string Login { get; set; }

        [Required]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}