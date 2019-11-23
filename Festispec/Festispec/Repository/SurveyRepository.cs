using Festispec.Interface;
using Festispec.ViewModel.survey;
using System.Collections.Generic;

namespace Festispec.Repository
{
    public class SurveyRepository
    {
        public List<IQuestion> GetQuestions(SurveyVM survey)
        {
            var result = new List<IQuestion>(survey.Questions);
            return result;
        }
    }
}
