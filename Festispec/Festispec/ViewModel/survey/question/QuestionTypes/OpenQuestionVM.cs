using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.survey.question.questionTypes;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class OpenQuestionVM : SurveyQuestionVM, IQuestion
    {
        public string QuestionString { get; set; }

        private void GetQuestion()
        {
            // Haal de question uit de json
            var json = base.Question;
            QuestionString = "";
        }
    }
}
