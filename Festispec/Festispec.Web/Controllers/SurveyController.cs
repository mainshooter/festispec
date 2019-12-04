using System.Linq;
using System.Net;
using System.Web.Mvc;
using Festispec.Domain;
using Festispec.Lib.Survey.Question;
using Festispec.Web.Models;
using Festispec.Web.Models.Questions;
using Festispec.Web.Models.Questions.Types;
using Newtonsoft.Json;

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

            var model = new SurveyModel {Survey = _db.Surveys.Find(id)};
            var repo = new QuestionTypeRepository();

            if (model.Survey == null)
                return HttpNotFound();

            foreach (var q in model.Survey.Questions)
            {
                var qType = repo.GetQuestionType(q.Type);
                qType.Details = JsonConvert.DeserializeObject<QuestionDetails>(q.Question1);
                qType.Id = q.Id;
                model.Questions.Add(qType);
            }
           
            return View(model);
        }

        public ActionResult Conduct(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new SurveyModel { Survey = _db.Surveys.Find(id) };
            var repo = new QuestionTypeRepository();

            if (model.Survey == null)
                return HttpNotFound();

            foreach (var q in model.Survey.Questions)
            {
                var qType = repo.GetQuestionType(q.Type);
                qType.Details = JsonConvert.DeserializeObject<QuestionDetails>(q.Question1);
                qType.Id = q.Id;
                model.Questions.Add(qType);
            }

            return View(model);
        }
    }
}