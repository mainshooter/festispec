﻿using Festispec.Domain;
using Festispec.Interface;
using Festispec.Message;
using Festispec.View.Pages.Survey.QuestionTypes.ClosedQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.CommentField;
using Festispec.View.Pages.Survey.QuestionTypes.DrawQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.ImageGalleryQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.MultipleChoiceQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.OpenQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.SliderQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.TableQuestion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.survey
{
    public class SurveyInfoVM: ViewModelBase
    {
        private SurveyVM _surveyVM;
        private Dictionary<string, Type> _editQuestionPageTypes;
        private Dictionary<string, Type> _addQuestionPageTypes;

        public SurveyVM SurveyVM
        { 
            get => _surveyVM;
            set
            {
                _surveyVM = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> QuestionTypes { get; set; }
        public string SelectedQuestionType { get; set; }
        public IQuestion SelectedQuestion { get; set; }
        public ICommand AddQuestionCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }

        public SurveyInfoVM()
        {
            _editQuestionPageTypes = new Dictionary<string, Type>() {
                ["Gesloten vraag"] = typeof(EditClosedQuestionPage),
                ["Opmerking vraag"] = typeof(EditCommentFieldPage),
                ["Teken vraag"] = typeof(EditDrawQuestionPage),
                ["Afbeelding galerij vraag"] = typeof(EditImageGalleryQuestionPage),
                ["Meerkeuze vraag"] = typeof(EditMultipleChoiceQuestionPage),
                ["Open vraag"] = typeof(EditOpenQuestionPage),
                ["Schuifbalk vraag"] = typeof(EditSliderQuestionPage),
                ["Tabel vraag"] = typeof(EditTableQuestionPage),
            };

            _addQuestionPageTypes = new Dictionary<string, Type>()
            {
                ["Gesloten vraag"] = typeof(AddClosedQuestionPage),
                ["Opmerking vraag"] = typeof(AddCommentFieldPage),
                ["Teken vraag"] = typeof(AddDrawQuestionPage),
                ["Afbeelding galerij vraag"] = typeof(AddImageGalleryQuestionPage),
                ["Meerkeuze vraag"] = typeof(AddMultipleChoiceQuestionPage),
                ["Open vraag"] = typeof(AddOpenQuestionPage),
                ["Schuifbalk vraag"] = typeof(AddSliderQuestionPage),
                ["Tabel vraag"] = typeof(AddTableQuestionPage),
            };

            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => {
                SurveyVM = message.NextSurvey;
            });

            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            EditQuestionCommand = new RelayCommand(OpenEditQuestion);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);

            GetQuestionTypes();
        }

        private void GetQuestionTypes()
        {
            QuestionTypes = new ObservableCollection<string>();
            using (var context = new Entities())
            {
                var questionTypes = context.QuestionTypes.ToList();

                foreach (var questionType in questionTypes)
                {
                    QuestionTypes.Add(questionType.Type);
                }

                SelectedQuestionType = QuestionTypes[0];
            }
        }

        private void OpenAddQuestion()
        {
            var surveyPageType = _addQuestionPageTypes[SelectedQuestionType];
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = surveyPageType});
            MessengerInstance.Send<ChangeSelectedSurveyMessage>(new ChangeSelectedSurveyMessage() { NextSurvey = SurveyVM});
        }

        private void OpenEditQuestion()
        {
            var surveyPageType = _editQuestionPageTypes[SelectedQuestion.QuestionType];
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = surveyPageType });
            MessengerInstance.Send<ChangeSelectedSurveyQuestionMessage>(new ChangeSelectedSurveyQuestionMessage() { NextQuestion = SelectedQuestion.ToModel(), SurveyVM = _surveyVM });
        }

        private void DeleteQuestion()
        {
            using (var context = new Entities())
            {
                context.Questions.Attach(SelectedQuestion.ToModel());
                context.Questions.Remove(SelectedQuestion.ToModel());
                context.SaveChanges();
                SurveyVM.Questions.Remove(SelectedQuestion);
            }
        }
    }
}
