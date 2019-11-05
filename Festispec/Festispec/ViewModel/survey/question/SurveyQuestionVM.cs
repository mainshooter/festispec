using Festispec.ViewModel.survey.question.questionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey.question
{
    public class SurveyQuestionVM
    {
        public int Id { get; set; }
        public SurveyVM Survey { get; set; }
        public IQuestion QuestionType { get; set; }

        public string Question { get; set; }
        public int Order { get; set; }
        public string Variable { get; set; }
    }
}
