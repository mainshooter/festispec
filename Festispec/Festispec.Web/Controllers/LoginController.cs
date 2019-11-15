using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Festispec.Domain;
using Festispec.Lib.Auth;
using Festispec.Lib.Interfaces;

namespace Festispec.Web.Controllers
{
    
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        { 
           ActionResult result = View();

           if (ModelState.IsValid)
           {
               using (var context = new Entities())
               {
                   var employee = context.Employees.FirstOrDefault(e => e.Email == email);
                   IPasswordValidator passwordService = new PasswordService();

                   if (employee == null)
                   {
                       ModelState.AddModelError("", "Gebruiker niet gevonden met de ingevoerde email.");
                   }
                   else if (!passwordService.PasswordsCompare(password, employee.Password))
                   {
                       ModelState.AddModelError("", "Wachtwoord ongeldig");
                   }
                   else
                   {
                       Session["loggedInUser"] = employee;
                       Session["loggedIn"] = true;

                       //Redirect naar pagina
                       result = RedirectToAction("Index", "Home");
                   }
               }
           }

           return result;
        }
    }
}