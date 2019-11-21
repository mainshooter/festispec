using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.OpenQuestion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.OpenQuestion
{
    public class AddOpenQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private OpenQuestionVM _openQuestionVm;

        public OpenQuestionVM OpenQuestionVm
        {
            get => _openQuestionVm;
            set
            {
                _openQuestionVm = value;
                RaisePropertyChanged(() => OpenQuestionVm);
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public AddOpenQuestionVM()
        {
            OpenQuestionVm = new OpenQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddOpenQuestionPage))
                {
                    OpenQuestionVm = new OpenQuestionVM();
                }
            });

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(OpenQuestionVm.GoBack);
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!OpenQuestionVm.ValidateQuestionDetails()) return;

                OpenQuestionVm.Question = JsonConvert.SerializeObject(OpenQuestionVm.QuestionDetails);
                OpenQuestionVm.Variables = StringToSlug.Slugify(OpenQuestionVm.QuestionDetails.Question);
                OpenQuestionVm.Type = "Open vraag";
                OpenQuestionVm.SurveyId = _surveyVm.Id;
                context.Questions.Add(OpenQuestionVm.ToModel());
                _surveyVm.Questions.Add(OpenQuestionVm);
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
