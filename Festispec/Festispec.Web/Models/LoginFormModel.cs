using System.ComponentModel.DataAnnotations;

namespace Festispec.Web.Models
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