using System.ComponentModel.DataAnnotations;

namespace Festispec.Web.ViewModels
{
    public class LoginFormViewModel
    {
        [Required(ErrorMessage = "Email niet ingevuld")]
        [EmailAddress(ErrorMessage = "Ongeldige email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord niet ingevuld")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}