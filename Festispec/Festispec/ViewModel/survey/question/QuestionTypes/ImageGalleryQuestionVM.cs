﻿using System;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Slugify;
using Festispec.Lib.Survey.Question;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class ImageGalleryQuestionVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private string _description;
        private int _maxImages;
        private string _questionType;
        private QuestionDetails _questionDetails;

        public QuestionDetails QuestionDetails
        { 
            get {
                return _questionDetails;
            }
            set {
                _questionDetails = value;
                RaisePropertyChanged();
            }
        }

        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public int MaxImages { get; set; }

        [PreferredConstructor]
        public ImageGalleryQuestionVM()
        {
            _questionType = "Afbeelding galerij vraag";
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message => {
                _surveyVm = message.SurveyVM;
                _surveyQuestion = message.NextQuestion;
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
                _question = QuestionDetails.Question;
                _description = QuestionDetails.Description;
                try
                {
                    MaxImages = Convert.ToInt32(QuestionDetails.Choices.Cols[0]);
                    _maxImages = MaxImages;
                }
                catch (Exception)
                {
                }

                
            });
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => {
                _surveyVm = message.NextSurvey;
                _surveyQuestion = new Question();
                QuestionDetails = new QuestionDetails();
            });
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
        }

        public ImageGalleryQuestionVM(SurveyVM surveyVm, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;

            if (_surveyQuestion.Question1 != null)
            {
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
                MaxImages = Convert.ToInt32(QuestionDetails.Choices.Cols[0]);
            }
            else
            {
                QuestionDetails = new QuestionDetails();
            }

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);

            // temp variables for when you want to go back and discard changes
            _question = QuestionDetails.Question;
            _description = QuestionDetails.Description;
            _maxImages = MaxImages;
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                QuestionDetails.Choices.Cols.Clear();
                QuestionDetails.Choices.Cols.Add(MaxImages.ToString());
                _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);

                if (_surveyQuestion.Id == 0)
                {
                    _question = QuestionDetails.Question;
                    _description = QuestionDetails.Description;
                    _maxImages = MaxImages;
                    _surveyQuestion.Variables = StringToSlug.Slugify(QuestionDetails.Question);
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

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }

        public void GoBack()
        {
            QuestionDetails.Question = _question;
            QuestionDetails.Description = _description;
            MaxImages = _maxImages;
            RaisePropertyChanged("QuestionDetails");
            RaisePropertyChanged("MaxImages");
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

            if (MaxImages > 20)
            {
                MessageBox.Show("Het maximum aantal afbeeldingen is 20.");
                return false;
            }

            if (MaxImages <= 0)
            {
                MessageBox.Show("Het minimum aantal afbeeldingen is 1.");
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
