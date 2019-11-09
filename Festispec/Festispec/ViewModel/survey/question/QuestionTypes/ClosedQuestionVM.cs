using Festispec.ViewModel.survey.question.questionTypes;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class ClosedQuestionVM : IQuestion
    {
        private SurveyQuestionVM _surveyQuestion;

        public string QuestionString { get; set; }

        public ClosedQuestionVM(SurveyQuestionVM surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
        }

        public void GetQuestion()
        {
            // Haal de question uit de json
            var json = _surveyQuestion.Question;
            QuestionString = "";
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}
