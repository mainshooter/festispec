using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Festispec.Web.ViewModels
{
    public class LoginFormModel
    {
        [Required(ErrorMessage = "Email niet ingevuld")]
        [EmailAddress(ErrorMessage = "Ongeldige email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord niet ingevuld")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}