using System.Linq;
using System.Net;
using System.Web.Mvc;
using Festispec.Domain;

namespace Festispec.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: Survey
        public ActionResult Index()
        {
            return View(_db.Surveys.ToList());
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var survey = _db.Surveys.Find(id);

            if (survey == null)
                return HttpNotFound();

            return View(survey);
        }
    }
}