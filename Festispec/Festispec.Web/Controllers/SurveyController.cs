using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Festispec.Domain;
using Festispec.Lib.Enums;
using Festispec.Lib.Survey.Question;
using Festispec.Web.Models;
using Festispec.Web.Models.Auth;
using Festispec.Web.Models.Questions;
using Newtonsoft.Json;
using System;

namespace Festispec.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly Entities _db = new Entities();
        
        // GET: Survey
        public ActionResult Index()
        {
            if (UserSession.Current.Employee == null)
            {
                return Redirect("~/User/Login");
            }

            //var surveysTodayWithOrderAndEvent = _db.Surveys.Include("Order.Event")
            //                        .Where(s => DateTime.Today >= s.Order.Event.BeginDate &&
            //                        DateTime.Today <= s.Order.Event.EndDate &&
            //                        s.Status == SurveyStatus.Definitief.ToString())
            //                        .ToList();
            var surveysTodayWithOrderAndEvent = _db.Surveys.Include("Order.Event").ToList();

            CheckAllowenceCurrentEmployeeWithSurveys(surveysTodayWithOrderAndEvent);

            return View(surveysTodayWithOrderAndEvent);
        }

        private List<Survey> CheckAllowenceCurrentEmployeeWithSurveys(List<Survey> surveysList)
        {
            foreach(var s in surveysList.ToList())
            {
                if(_db.InspectorPlannings.ToList().Any(i => i.EmployeeId == UserSession.Current.Employee.Id && i.OrderId == s.Order.Id) == false && UserSession.Current.Employee.Department != "Directie")
                {
                    surveysList.Remove(s);
                }
            }

            return surveysList;
        }

        public ActionResult Details(int? id)
        {
            if(UserSession.Current.Employee == null)
            {
                return Redirect("~/User/Login");
            }

            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new SurveyModel {Survey = _db.Surveys.Find(id)};
            
            if (model.Survey == null)
                return HttpNotFound();
            
            if (CheckAllowenceCurrentEmployeeWithSurveys(new List<Survey> { model.Survey }).Count == 0)
            {
                Response.Redirect("~/Survey");
            }

            foreach (var q in model.Survey.Questions)
            {
                var qType = QuestionTypeFactory.CreateQuestionTypeFor(q.Type);
                qType.Details = JsonConvert.DeserializeObject<QuestionDetails>(q.Question1);
                qType.DetailsJson = q.Question1;
                qType.Id = q.Id;
                model.Questions.Add(qType);
            }
           
            return View(model);
        }

        [HttpGet]
        public ActionResult Conduct(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new SurveyModel { Survey = _db.Surveys.Find(id) };
            var questionVars = new List<string>();
            var repo = new QuestionTypeRepository();

            if (model.Survey == null)
                return HttpNotFound();
            var question = model.Survey.Questions;
            foreach (var q in question)
            {
                var qType = repo.GetQuestionType(q.Type);
                qType.Details = JsonConvert.DeserializeObject<QuestionDetails>(q.Question1);
                qType.Id = q.Id;
                qType.Variable = q.Variables;
                questionVars.Add(q.Variables);
                model.Questions.Add(qType);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Conduct(int id)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            string[] keys = Request.Form.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                if (key == "")
                {
                    continue;
                }
                request[key] = Request.Form[keys[i]];
            }
            Survey survey = _db.Surveys.Find(id);
            List<Question> questions = survey.Questions.ToList();
            List<Answer> answers = new List<Answer>();
            Case surveyCase = new Case() { Survey = survey, EmployeeId = 1, Status = "Klaar" };
            foreach (var givenAnswer in request)
            {
                string questionVar = givenAnswer.Key;
                questionVar = questionVar.Replace("question.", "");
                string questionAnswer = givenAnswer.Value;

                Question question = questions.Where(q => q.Variables.Equals(questionVar)).FirstOrDefault();
                Answer answer = new Answer() { Case = surveyCase, QuestionId = question.Id, Answer1 = questionAnswer };
                answers.Add(answer);
            }
            _db.Cases.Add(surveyCase);
            _db.Answers.AddRange(answers);
            _db.SaveChanges();


            return Json(new { }, JsonRequestBehavior.DenyGet);
        }
    }
}