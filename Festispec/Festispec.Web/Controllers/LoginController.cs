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
        public HttpResponseMessage Login(string email, string password)
        {
            Employee result = null;

            using (var context = new Entities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.Email == email);
                var passwordService = new PasswordService();

                if (employee == null)
                {
                    //TODO
                }
                else if (!passwordService.PasswordsCompare(password, employee.Password))
                {
                    //TODO
                }
                else
                {
                    Session["loggedInUser"] = employee;
                    Session["loggedIn"] = true;
                    result = employee;
                }
            }

            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }
    }
}