using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.MultipleChoiceQuestion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.MultipleChoiceQuestion
{
    public class AddMultipleChoiceQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private MultipleChoiceQuestionVM _questionVm;

        public MultipleChoiceQuestionVM QuestionVm
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

        public AddMultipleChoiceQuestionVM()
        {
            QuestionVm = new MultipleChoiceQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddMultipleChoiceQuestionPage))
                {
                    QuestionVm = new MultipleChoiceQuestionVM();
                }
            });

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(QuestionVm.GoBack);
            AddOptionCommand = new RelayCommand(QuestionVm.AddOption);
            DeleteOptionCommand = new RelayCommand(QuestionVm.DeleteOption);
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!QuestionVm.ValidateQuestionDetails()) return;

                QuestionVm.Question = JsonConvert.SerializeObject(QuestionVm.QuestionDetails);
                QuestionVm.Variables = StringToSlug.Slugify(QuestionVm.QuestionDetails.Question);
                QuestionVm.Order = _surveyVm.Questions.Count + 1;
                QuestionVm.Type = "Meerkeuze vraag";
                QuestionVm.SurveyId = _surveyVm.Id;
                context.Questions.Add(QuestionVm.ToModel());
                _surveyVm.Questions.Add(QuestionVm);
                context.SaveChanges();
            }
            
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
