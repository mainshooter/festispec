using System.ComponentModel.DataAnnotations;

namespace Festispec.Web.Models
{
    public class RequestPasswordModel
    {
        [Required(ErrorMessage = "Email niet ingevuld")]
        [EmailAddress(ErrorMessage = "Ongeldige email")]
        public string Email { get; set; }
    }
}