using System;
using Festispec.Domain;
using Festispec.Model;
using Festispec.ViewModel.survey.question.questionTypes;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class ClosedQuestionVM : IQuestion
    {
        private Question _surveyQuestion;

        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType { get; }

        public ClosedQuestionVM()
        {
            _surveyQuestion = new Question();
        }

        public ClosedQuestionVM(Question surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
            QuestionType = _surveyQuestion.Type;
            QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
