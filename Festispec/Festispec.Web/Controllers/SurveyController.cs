using System.Linq;
using System.Net;
using System.Web.Mvc;
using Festispec.Domain;
using Festispec.Lib.Enums;
using Festispec.Lib.Survey.Question;
using Festispec.Web.Models;
using Festispec.Web.Models.Questions;
using Newtonsoft.Json;
using Festispec.Web.Models.Auth;
using System;
using System.Collections.Generic;

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

            var surveysTodayWithOrderAndEvent = _db.Surveys.Include("Order.Event")
                                    .Where(s => DateTime.Today >= s.Order.Event.BeginDate &&
                                    DateTime.Today <= s.Order.Event.EndDate &&
                                    s.Status == SurveyStatus.Definitief.ToString())
                                    .ToList();
            CheckAllowenceCurrentEmployeeWithSurveys(surveysTodayWithOrderAndEvent);

            return View(surveysTodayWithOrderAndEvent);
        }

        private List<Survey> CheckAllowenceCurrentEmployeeWithSurveys(List<Survey> surveysList)
        {
            foreach(var s in surveysList.ToList())
            {
                if(_db.InspectorPlannings.ToList().Any(i => i.EmployeeId == UserSession.Current.Employee.Id && i.OrderId == s.Order.Id) == false)
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
    }
}