﻿using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.CommentField
{
    public class EditCommentFieldVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private CommentFieldVM _questionVm;
        private ICommand _saveCommand;
        private ICommand _goBackCommand;

        public CommentFieldVM QuestionVm
        {
            get => _questionVm;
            set
            {
                _questionVm = value;
                RaisePropertyChanged(() => QuestionVm);
            }
        }

        public ICommand SaveCommand
        {
            get => _saveCommand;
            set
            {
                _saveCommand = value;
                RaisePropertyChanged(() => SaveCommand);
            }
        }

        public ICommand GoBackCommand
        {
            get => _goBackCommand;
            set
            {
                _goBackCommand = value;
                RaisePropertyChanged(() => GoBackCommand);
            }
        }

        public EditCommentFieldVM()
        {
            QuestionVm = new CommentFieldVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message =>
            {
                if (message.NextQuestion.GetType() == typeof(CommentFieldVM))
                {
                    _surveyVm = message.SurveyVM;
                    QuestionVm = (CommentFieldVM)message.NextQuestion;
                    SaveCommand = new RelayCommand(Save);
                    GoBackCommand = new RelayCommand(QuestionVm.GoBack);
                }
            });
            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(SurveyPage) && _surveyVm.Questions != null)
                {
                    _surveyVm.RefreshQuestions();
                }
            });
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!QuestionVm.ValidateQuestionDetails()) return;

                QuestionVm.Question = JsonConvert.SerializeObject(QuestionVm.QuestionDetails);
                QuestionVm.Variables = StringToSlug.Slugify(QuestionVm.QuestionDetails.Question);
                context.Questions.Attach(QuestionVm.ToModel());
                context.Entry(QuestionVm.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
