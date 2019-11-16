using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Survey.Question;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class DrawQuestionVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private string _description;
        private byte[] _image;
        private string _questionType;
        private QuestionDetails _questionDetails;

        public QuestionDetails QuestionDetails
        { 
            get => _questionDetails;
            set {
                _questionDetails = value;
                RaisePropertyChanged();
            }
        }

        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand AddImageCommand { get; set; }

        [PreferredConstructor]
        public DrawQuestionVM()
        {
            _questionType = "Teken vraag";
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message => {
                _surveyVm = message.SurveyVM;
                _surveyQuestion = message.NextQuestion;
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
                _question = QuestionDetails.Question;
                _description = QuestionDetails.Description;
                try
                {
                    _image = QuestionDetails.Images[0];
                }
                catch (Exception)
                {
                }
            });
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => {
                _surveyVm = message.NextSurvey;
                QuestionDetails = new QuestionDetails();
                _surveyQuestion = new Question();
            });
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
            AddImageCommand = new RelayCommand(AddImage);
        }

        public DrawQuestionVM(SurveyVM surveyVm, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;

            if (_surveyQuestion.Question1 != null)
            {
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
            }
            else
            {
                QuestionDetails = new QuestionDetails();
                QuestionDetails.Images.Add(new byte[0]);
            }

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
            AddImageCommand = new RelayCommand(AddImage);

            // temp variables for when you want to go back and discard changes
            _question = QuestionDetails.Question;
            _description = QuestionDetails.Description;
            _image = QuestionDetails.Images[0];
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);

                if (_surveyQuestion.Id == 0)
                {
                    _question = QuestionDetails.Question;
                    _description = QuestionDetails.Description;
                    _image = QuestionDetails.Images[0];
                    _surveyQuestion.Variables = "test";
                    _surveyQuestion.Type = _questionType;
                    _surveyQuestion.SurveyId = _surveyVm.ToModel().Id;
                    context.Questions.Add(_surveyQuestion);
                    _surveyVm.Questions.Add(this);
                    context.SaveChanges();
                }
                else
                {
                    context.Questions.Attach(_surveyQuestion);
                    context.Entry(_surveyQuestion).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage)});
        }

        public void GoBack()
        {
            QuestionDetails.Question = _question;
            QuestionDetails.Description = _description;
            QuestionDetails.Images[0] = _image;
            RaisePropertyChanged("QuestionDetails");
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

        private void AddImage()
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
                QuestionDetails.Images.Clear();
                QuestionDetails.Images.Add(image);
                RaisePropertyChanged("QuestionDetails");
            }
        }
    }
}
