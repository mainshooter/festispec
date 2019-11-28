using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.ClosedQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.OpenQuestion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.ClosedQuestion
{
    public class AddClosedQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private ClosedQuestionVM _questionVm;

        public ClosedQuestionVM QuestionVm
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

        public AddClosedQuestionVM()
        {
            QuestionVm = new ClosedQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddClosedQuestionPage))
                {
                    QuestionVm = new ClosedQuestionVM();
                }
            });

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(QuestionVm.GoBack);
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!QuestionVm.ValidateQuestionDetails()) return;

                QuestionVm.QuestionDetails.Choices.Cols.Add("Ja");
                QuestionVm.QuestionDetails.Choices.Cols.Add("Nee");
                QuestionVm.Question = JsonConvert.SerializeObject(QuestionVm.QuestionDetails);
                QuestionVm.Variables = StringToSlug.Slugify(QuestionVm.QuestionDetails.Question);
                QuestionVm.Type = "Gesloten vraag";
                QuestionVm.SurveyId = _surveyVm.Id;
                context.Questions.Add(QuestionVm.ToModel());
                _surveyVm.Questions.Add(QuestionVm);
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
