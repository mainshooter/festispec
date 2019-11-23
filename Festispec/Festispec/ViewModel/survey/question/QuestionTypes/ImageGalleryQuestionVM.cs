using System;
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
    public class ImageGalleryQuestionVM : ViewModelBase, IQuestion
    {
        private readonly Question _surveyQuestion;

        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType => _surveyQuestion.Type;

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

        [PreferredConstructor]
        public ImageGalleryQuestionVM()
        {
            _surveyQuestion = new Question();
            QuestionDetails = new QuestionDetails();
            QuestionDetails.Choices.Cols.Add("");
        }

        public ImageGalleryQuestionVM(Question surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
            QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
        }

        public void GoBack()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
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

            try
            {
                if (Convert.ToInt32(QuestionDetails.Choices.Cols[0]) > 20)
                {
                    MessageBox.Show("Het maximum aantal afbeeldingen is 20.");
                    return false;
                }

                if (Convert.ToInt32(QuestionDetails.Choices.Cols[0]) <= 0)
                {
                    MessageBox.Show("Het minimum aantal afbeeldingen is 1.");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Voer een getal in bij limiet aantal afbeeldingen.");
                return false;
            }

            return true;
        }

        public Question ToModel()
        {
            return _surveyQuestion;
        }
    }
}
