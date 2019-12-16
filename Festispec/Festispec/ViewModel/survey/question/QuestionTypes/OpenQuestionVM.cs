using System.Windows;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Survey.Question;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class OpenQuestionVM : ViewModelBase, IQuestion
    {
        private readonly Question _surveyQuestion;

        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType => _surveyQuestion.Type;

        public int Id {
            get {
                return _surveyQuestion.Id;
            }
        }

        public int SurveyId
        {
            get => _surveyQuestion.SurveyId;
            set => _surveyQuestion.SurveyId = value;
        }

        public string Question
        {
            get => _surveyQuestion.Question1;
            set => _surveyQuestion.Question1 = value;
        }

        public string Variables
        {
            get => _surveyQuestion.Variables;
            set => _surveyQuestion.Variables = value;
        }

        public string Type
        {
            get => _surveyQuestion.Type;
            set => _surveyQuestion.Type = value;
        }

        public int Order
        {
            get => _surveyQuestion.Order;
            set => _surveyQuestion.Order = value;
        }

        [PreferredConstructor]
        public OpenQuestionVM()
        {
            _surveyQuestion = new Question();
            Type = Lib.Enums.QuestionType.OpenQuestion;
            QuestionDetails = new QuestionDetails();
        }

        public OpenQuestionVM(Question surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
            QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
        }

        public bool ValidateQuestionDetails()
        {
            if (QuestionDetails.Question == "" || QuestionDetails.Question.Length > 255)
            {
                MessageBox.Show("De vraag mag niet leeg zijn of langer zijn dan 255 karakters.");
                return false;
            }

            if (QuestionDetails.Description.Length > 500)
            {
                MessageBox.Show("De omschrijving mag niet langer zijn dan 500 karakters.");
                return false;
            }

            return true;
        }

        public void GoBack()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }

        public Question ToModel()
        {
            return _surveyQuestion;
        }
    }
}
