using System.Web.Mvc;
using Festispec.Lib.Auth;

namespace Festispec.Web.Controllers
{
    public class PasswordController : Controller
    {
        [HttpGet]
        public ActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reset(string email, string password)
        {
            if (password.Length > 99999 || password.Length <= 0)
            {
                ModelState.AddModelError("", "Lengte van wachtwoord ongeldig.");
                return View();
            }

            var result = PasswordResetService.UpdatePasswordFor(email, password);

            if (result)
            {
                return RedirectToAction("Login", "User");
            }

            ModelState.AddModelError("", "Fout bij het bijwerken van het wachtwoord.");
            return View();
        }

        [HttpGet]
        public ActionResult RequestNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestNew(string email)
        {
            if (PasswordResetService.AccountExists(email))
            {
                var code = PasswordResetService.GenerateResetCode();
                PasswordResetService.SaveResetCodeFor(email, code);
                PasswordResetService.SendEmailWithResetCode(email, code);

                return RedirectToAction("VerifyCode");
            }
            else
            {
                ModelState.AddModelError("", "Gebruiker niet gevonden.");
                return View();
            }
        }

        public ActionResult VerifyCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyCode(string code)
        {
            if (PasswordResetService.ValidateResetCode(code))
            {
                return RedirectToAction("Reset");
            } 

            ModelState.AddModelError("", "Code ongeldig.");
            return View();
        }
    }
}