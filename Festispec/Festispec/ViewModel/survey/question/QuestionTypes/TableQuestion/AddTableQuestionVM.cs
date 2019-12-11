using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.TableQuestion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.TableQuestion
{
    public class AddTableQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private TableQuestionVM _questionVm;

        public TableQuestionVM QuestionVm
        {
            get => _questionVm;
            set
            {
                _questionVm = value;
                RaisePropertyChanged(() => QuestionVm);
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand AddOptionCommand { get; set; }
        public ICommand DeleteOptionCommand { get; set; }
        public ICommand AddColumnCommand { get; set; }
        public ICommand DeleteColumnCommand { get; set; }

        public AddTableQuestionVM()
        {
            QuestionVm = new TableQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddTableQuestionPage))
                {
                    QuestionVm = new TableQuestionVM();
                }
            });

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
            AddOptionCommand = new RelayCommand(AddOption, CanAddOption);
            DeleteOptionCommand = new RelayCommand(DeleteOption);
            AddColumnCommand = new RelayCommand(AddColumn);
            DeleteColumnCommand = new RelayCommand(DeleteColumn);
        }

        public void DeleteColumn()
        {
            QuestionVm.DeleteColumn();
        }

        public void AddColumn()
        {
            QuestionVm.AddColumn();
        }

        public void DeleteOption()
        {
            QuestionVm.DeleteOption();
        }

        public bool CanAddOption()
        {
            return QuestionVm.CanAddOption();
        }

        public void AddOption()
        {
            QuestionVm.AddOption();
        }

        public void GoBack()
        {
            QuestionVm.GoBack();
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!QuestionVm.ValidateQuestionDetails()) return;

                QuestionVm.Question = JsonConvert.SerializeObject(QuestionVm.QuestionDetails);
                QuestionVm.Variables = StringToSlug.Slugify(QuestionVm.QuestionDetails.Question);
                QuestionVm.Order = _surveyVm.Questions.Count + 1;
                QuestionVm.Type = "Tabel vraag";
                QuestionVm.SurveyId = _surveyVm.Id;
                context.Questions.Add(QuestionVm.ToModel());
                _surveyVm.Questions.Add(QuestionVm);
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
