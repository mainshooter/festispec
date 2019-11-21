using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.ClosedQuestion
{
    public class EditClosedQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private ClosedQuestionVM _closedQuestionVm;
        private ICommand _saveCommand;
        private ICommand _goBackCommand;

        public ClosedQuestionVM ClosedQuestionVm
        {
            get => _closedQuestionVm;
            set
            {
                _closedQuestionVm = value;
                RaisePropertyChanged(() => ClosedQuestionVm);
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
        
        public EditClosedQuestionVM()
        {
            ClosedQuestionVm = new ClosedQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message =>
            {
                if (message.NextQuestion.GetType() == typeof(ClosedQuestionVM))
                {
                    _surveyVm = message.SurveyVM;
                    ClosedQuestionVm = (ClosedQuestionVM) message.NextQuestion;
                    SaveCommand = new RelayCommand(Save);
                    GoBackCommand = new RelayCommand(ClosedQuestionVm.GoBack);
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
                if (!ClosedQuestionVm.ValidateQuestionDetails()) return;

                ClosedQuestionVm.Question = JsonConvert.SerializeObject(ClosedQuestionVm.QuestionDetails);
                ClosedQuestionVm.Variables = StringToSlug.Slugify(ClosedQuestionVm.QuestionDetails.Question);
                context.Questions.Attach(ClosedQuestionVm.ToModel());
                context.Entry(ClosedQuestionVm.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
