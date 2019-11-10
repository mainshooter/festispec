using System;
using Festispec.Domain;
using Festispec.ViewModel.survey.question.questionTypes;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class OpenQuestionVM : IQuestion
    {
        private Question _surveyQuestion;

        public string QuestionString { get; set; }
        public string QuestionType { get; }

        public OpenQuestionVM()
        {
            _surveyQuestion = new Question();
        }

        public OpenQuestionVM(Question surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
            QuestionType = _surveyQuestion.Type;
            GetQuestion();
        }

        public void GetQuestion()
        {
            // Haal de question uit de json
            var json = _surveyQuestion.Question1;
            QuestionString = "test 1";
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
