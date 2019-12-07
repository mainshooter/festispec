using System.Collections.Generic;
using Festispec.Domain;
using Festispec.Web.Models.Questions;

namespace Festispec.Web.Models
{
    public class SurveyModel
    {
        public Survey Survey { get; set; }
        public ICollection<IQuestion> Questions { get; } = new List<IQuestion>();

        public string QuestionVars { get; set; }
    }
}