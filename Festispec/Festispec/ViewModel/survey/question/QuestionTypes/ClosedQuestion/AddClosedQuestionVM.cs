using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.OpenQuestion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.ClosedQuestion
{
    public class AddClosedQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private ClosedQuestionVM _closedQuestionVm;

        public ClosedQuestionVM ClosedQuestionVm
        {
            get => _closedQuestionVm;
            set
            {
                _closedQuestionVm = value;
                RaisePropertyChanged(() => ClosedQuestionVm);
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public AddClosedQuestionVM()
        {
            ClosedQuestionVm = new ClosedQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddOpenQuestionPage))
                {
                    ClosedQuestionVm = new ClosedQuestionVM();
                }
            });

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(ClosedQuestionVm.GoBack);
        }
        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ClosedQuestionVm.ValidateQuestionDetails()) return;

                ClosedQuestionVm.QuestionDetails.Choices.Cols.Add("Ja");
                ClosedQuestionVm.QuestionDetails.Choices.Cols.Add("Nee");
                ClosedQuestionVm.Question = JsonConvert.SerializeObject(ClosedQuestionVm.QuestionDetails);
                ClosedQuestionVm.Variables = StringToSlug.Slugify(ClosedQuestionVm.QuestionDetails.Question);
                ClosedQuestionVm.Type = "Gesloten vraag";
                ClosedQuestionVm.SurveyId = _surveyVm.Id;
                context.Questions.Add(ClosedQuestionVm.ToModel());
                _surveyVm.Questions.Add(ClosedQuestionVm);
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
