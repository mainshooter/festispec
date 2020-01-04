using System;
using System.IO;
using System.Windows;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Survey.Question;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class DrawQuestionVM : ViewModelBase, IQuestion
    {
        private readonly Question _surveyQuestion;

        public QuestionDetails QuestionDetails { get; set; }

        public string QuestionType => _surveyQuestion.Type;

        public int Id 
        {
            get 
            {
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
        public DrawQuestionVM()
        {
            _surveyQuestion = new Question();
            QuestionDetails = new QuestionDetails();
            Type = Lib.Enums.QuestionType.DrawQuestion;
            QuestionDetails.Images.Add(new byte[0]);
        }

        public DrawQuestionVM(Question surveyQuestion)
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

            if (QuestionDetails.Images[0].Length == 0)
            {
                MessageBox.Show("Voeg een afbeelding toe.");
                return false;
            }

            return true;
        }

        public Question ToModel()
        {
            return _surveyQuestion;
        }

        public void AddImage()
        {
            var fd = new OpenFileDialog { Filter = "All Image Files | *.*", Multiselect = false };

            if (fd.ShowDialog() != true) return;

            using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length > 10000000)
                {
                    MessageBox.Show("De afbeelding bestandsgrootte mag niet groter zijn dan 10 MB.");
                    return;
                }

                var image = new byte[fs.Length];
                fs.Read(image, 0, Convert.ToInt32(fs.Length));
                QuestionDetails.Images[0] = image;
                RaisePropertyChanged(() => QuestionDetails);
            }
        }
    }
}
