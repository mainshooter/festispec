using Festispec.ViewModel.survey;
using Festispec.ViewModel.survey.question;
using Festispec.ViewModel.survey.question.questionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Repository
{
    public class SurveyRepository
    {
        public List<SurveyQuestionVM> GetQuestions(SurveyVM survey)
        {
            var result = new List<SurveyQuestionVM>(survey.Questions);
            return result;
        }
    }
}
