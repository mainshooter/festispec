using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.OpenQuestion
{
    public class EditOpenQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private OpenQuestionVM _openQuestionVm;
        private ICommand _saveCommand;
        private ICommand _goBackCommand;

        public OpenQuestionVM OpenQuestionVm
        {
            get => _openQuestionVm;
            set
            {
                _openQuestionVm = value;
                RaisePropertyChanged(() => OpenQuestionVm);
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

        public EditOpenQuestionVM()
        {
            OpenQuestionVm = new OpenQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message =>
            {
                if (message.NextQuestion.GetType() == typeof(OpenQuestionVM))
                {
                    _surveyVm = message.SurveyVM;
                    OpenQuestionVm = (OpenQuestionVM) message.NextQuestion;
                    SaveCommand = new RelayCommand(Save);
                    GoBackCommand = new RelayCommand(OpenQuestionVm.GoBack);
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
                if (!OpenQuestionVm.ValidateQuestionDetails()) return;

                OpenQuestionVm.Question = JsonConvert.SerializeObject(OpenQuestionVm.QuestionDetails);
                OpenQuestionVm.Variables = StringToSlug.Slugify(OpenQuestionVm.QuestionDetails.Question);
                context.Questions.Attach(OpenQuestionVm.ToModel());
                context.Entry(OpenQuestionVm.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
