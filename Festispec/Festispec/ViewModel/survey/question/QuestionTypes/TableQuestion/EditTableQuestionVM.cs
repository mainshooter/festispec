using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.TableQuestion
{
    public class EditTableQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private TableQuestionVM _questionVm;
        private ICommand _saveCommand;
        private ICommand _goBackCommand;
        private ICommand _addOptionCommand;
        private ICommand _addColumnCommand;
        private ICommand _deleteOptionCommand;
        private ICommand _deleteColumnCommand;

        public TableQuestionVM QuestionVm
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

        public ICommand AddOptionCommand
        {
            get => _addOptionCommand;
            set
            {
                _addOptionCommand = value;
                RaisePropertyChanged(() => AddOptionCommand);
            }
        }

        public ICommand AddColumnCommand
        {
            get => _addColumnCommand;
            set
            {
                _addColumnCommand = value;
                RaisePropertyChanged(() => AddColumnCommand);
            }
        }

        public ICommand DeleteOptionCommand
        {
            get => _deleteOptionCommand;
            set
            {
                _deleteOptionCommand = value;
                RaisePropertyChanged(() => DeleteOptionCommand);
            }
        }

        public ICommand DeleteColumnCommand
        {
            get => _deleteColumnCommand;
            set
            {
                _deleteColumnCommand = value;
                RaisePropertyChanged(() => DeleteColumnCommand);
            }
        }

        public EditTableQuestionVM()
        {
            QuestionVm = new TableQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message =>
            {
                if (message.NextQuestion.GetType() == typeof(TableQuestionVM))
                {
                    _surveyVm = message.SurveyVM;
                    QuestionVm = (TableQuestionVM)message.NextQuestion;
                    SaveCommand = new RelayCommand(Save);
                    GoBackCommand = new RelayCommand(QuestionVm.GoBack);
                    AddOptionCommand = new RelayCommand(QuestionVm.AddOption);
                    AddColumnCommand = new RelayCommand(QuestionVm.AddColumn);
                    DeleteOptionCommand = new RelayCommand(QuestionVm.DeleteOption);
                    DeleteColumnCommand = new RelayCommand(QuestionVm.DeleteColumn);
                    QuestionVm.SetComboBox();
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
