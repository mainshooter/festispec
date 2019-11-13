using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Festispec.Domain;

namespace Festispec.Web.Controllers.Auth
{
    [RoutePrefix("login")]
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
            Employee result = null;

            using (var ctx = new Entities())
            {
                var employee = ctx.Employees.FirstOrDefault(i => i.Email == email);
            }

            return View(result);
        }
    }
}