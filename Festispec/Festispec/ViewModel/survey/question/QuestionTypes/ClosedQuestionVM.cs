using System;
using Festispec.Domain;
using Festispec.ViewModel.survey.question.questionTypes;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class ClosedQuestionVM : IQuestion
    {
        private Question _surveyQuestion;

        public string QuestionString { get; set; }
        public string QuestionType { get; }

        public ClosedQuestionVM()
        {
            _surveyQuestion = new Question();
        }

        public ClosedQuestionVM(Question surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
            QuestionType = _surveyQuestion.Type;
            GetQuestion();
        }

        public void GetQuestion()
        {
            // Haal de question uit de json
            var json = _surveyQuestion.Question1;
            QuestionString = "test";
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
