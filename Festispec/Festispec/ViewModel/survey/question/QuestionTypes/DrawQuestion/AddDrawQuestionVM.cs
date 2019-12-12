using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Slugify;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.DrawQuestion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes.DrawQuestion
{
    public class AddDrawQuestionVM : ViewModelBase
    {
        private SurveyVM _surveyVm;
        private DrawQuestionVM _questionVm;

        public DrawQuestionVM QuestionVm
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
        public ICommand AddImageCommand { get; set; }

        public AddDrawQuestionVM()
        {
            QuestionVm = new DrawQuestionVM();
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => { _surveyVm = message.NextSurvey; });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddDrawQuestionPage))
                {
                    QuestionVm = new DrawQuestionVM();
                }
            });

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
            AddImageCommand = new RelayCommand(AddImage);
        }

        public void AddImage()
        {
            QuestionVm.AddImage();
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
                QuestionVm.Type = "Teken vraag";
                QuestionVm.SurveyId = _surveyVm.Id;
                context.Questions.Add(QuestionVm.ToModel());
                _surveyVm.Questions.Add(QuestionVm);
                context.SaveChanges();
            }

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }
    }
}
