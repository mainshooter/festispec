using Festispec.Domain;
using Festispec.Web.Models;
using Festispec.Web.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Festispec.Web.Controllers
{
    public class EventController : Controller
    {

        private readonly Entities _db = new Entities();

        public ActionResult EventDetails(int? id)
        {
            if (UserSession.Current.Employee == null)
            {
                return Redirect("~/User/Login");
            }

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new EventModel { Event = _db.Events.Find(id) };

            if (model.Event == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

    }
}