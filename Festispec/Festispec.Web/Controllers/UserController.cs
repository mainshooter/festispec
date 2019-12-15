using System.Linq;
using System.Web.Mvc;
using Festispec.Domain;
using Festispec.Lib.Auth;
using Festispec.Lib.Interfaces;
using Festispec.Web.Models.Auth;

namespace Festispec.Web.Controllers
{
    
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var session = UserSession.Current;
            session.Clear();

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        { 
           ActionResult result = View();

           if (ModelState.IsValid)
           {
               using (var context = new Entities())
               {
                   var employee = context.Employees.FirstOrDefault(e => e.Email == "m.peeters@gmail.com");
                   IPasswordValidator passwordService = new PasswordService();

                   if (employee == null)
                   {
                       ModelState.AddModelError("", "Gebruiker niet gevonden met het ingevoerde emailadres.");
                   }
                   else if (!passwordService.PasswordsCompare("GamerBoy95", employee.Password))
                   {
                       ModelState.AddModelError("", "Wachtwoord ongeldig");
                   }
                   else
                   {
                       var userSession = UserSession.Current;
                       userSession.Employee = employee;

                       //Redirect naar pagina
                       result = RedirectToAction("Index", "Survey");
                   }
               }
           }

           return result;
        }
    }
}