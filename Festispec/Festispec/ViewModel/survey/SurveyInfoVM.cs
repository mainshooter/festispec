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
using System.Windows;
using System.Windows.Input;
using Festispec.View.Pages.Customer.Event;
using Festispec.ViewModel.toast;

namespace Festispec.ViewModel.survey
{
    public class SurveyInfoVM: ViewModelBase
    {
        private SurveyVM _surveyVM;
        private readonly Dictionary<string, Type> _editQuestionPageTypes;
        private readonly Dictionary<string, Type> _addQuestionPageTypes;

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
        public ICommand SaveCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand QuestionUpCommand { get; set; }
        public ICommand QuestionDownCommand { get; set; }
        public ICommand BackCommand { get; set; }

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

            AddQuestionCommand = new RelayCommand(OpenAddQuestion, IsConcept);
            EditQuestionCommand = new RelayCommand(OpenEditQuestion, IsConcept);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion, IsConcept);
            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset, IsConcept);
            QuestionUpCommand = new RelayCommand(MoveQuestionUp, IsConcept);
            QuestionDownCommand = new RelayCommand(MoveQuestionDown, IsConcept);
            BackCommand = new RelayCommand(Back);

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
            MessengerInstance.Send<ChangeSelectedSurveyMessage>(new ChangeSelectedSurveyMessage() { NextSurvey = SurveyVM });
        }

        private void OpenEditQuestion()
        {
            var surveyPageType = _editQuestionPageTypes[SelectedQuestion.QuestionType];
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = surveyPageType });
            MessengerInstance.Send<ChangeSelectedSurveyQuestionMessage>(new ChangeSelectedSurveyQuestionMessage() { NextQuestion = SelectedQuestion, SurveyVM = SurveyVM });
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

        private void Save()
        {
            using (var context = new Entities())
            {
                if (!SurveyVM.ValidateInputs()) return;

                context.Surveys.Attach(SurveyVM.ToModel());
                context.Entry(SurveyVM.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Vragenlijst bijgewerkt.");
        }

        private void Reset()
        {
            if (MessageBox.Show("Weet je zeker dat je alle vragen en bijbehoordende antwoorden wil verwijderen?", "Weet je het zeker?", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            using (var context = new Entities())
            {
                var surveyId = SurveyVM.ToModel().Id;
                var list = context.Questions.Where(q => q.SurveyId == surveyId);
                context.Questions.RemoveRange(list);
                context.SaveChanges();
                SurveyVM.OrderVM.ToModel().Surveys.Clear();
                SurveyVM.Questions.Clear();
            }

            CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Vragen verwijderd.");
        }

        private bool IsConcept()
        {
            return SurveyVM == null || SurveyVM.IsConcept();
        }

        private void MoveQuestionUp()
        {
            try
            {
                var aboveQuestion = SurveyVM.Questions[SelectedQuestion.Order - 2];
                var aboveQuestionOrder = aboveQuestion.Order;
                aboveQuestion.Order = SelectedQuestion.Order;
                SelectedQuestion.Order = aboveQuestionOrder;
                SurveyVM.Questions = new ObservableCollection<IQuestion>(SurveyVM.Questions.OrderBy(q => q.Order));
                SaveQuestionOrder(aboveQuestion);
            }
            catch (Exception)
            {
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowError("Deze vraag staat al bovenaan.");
            }
        }

        private void MoveQuestionDown()
        {
            try
            {
                var belowQuestion = SurveyVM.Questions[SelectedQuestion.Order];
                var aboveQuestionOrder = belowQuestion.Order;
                belowQuestion.Order = SelectedQuestion.Order;
                SelectedQuestion.Order = aboveQuestionOrder;
                SurveyVM.Questions = new ObservableCollection<IQuestion>(SurveyVM.Questions.OrderBy(q => q.Order));
                SaveQuestionOrder(belowQuestion);
            }
            catch (Exception)
            {
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowError("Deze vraag staat al onderaan.");
            }
        }

        private void SaveQuestionOrder(IQuestion questionToSwitchWithSelected)
        {
            using (var context = new Entities())
            {
                context.Questions.Attach(questionToSwitchWithSelected.ToModel());
                context.Entry(questionToSwitchWithSelected.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                context.Questions.Attach(SelectedQuestion.ToModel());
                context.Entry(SelectedQuestion.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }
    }
}
