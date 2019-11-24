using System.Windows.Input;
using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel.survey
{
    public class AddSurveyVM : ViewModelBase
    {
        private SurveyVM _surveyVM;

        public SurveyVM SurveyVM
        {
            get => _surveyVM;
            set
            {
                _surveyVM = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SaveEditCommand { get; set; }
        public ICommand SaveBackCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public AddSurveyVM()
        {
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => {
                SurveyVM = message.NextSurvey;
                SurveyVM.Status = SurveyVM.Statuses[0];
            });

            SaveEditCommand = new RelayCommand(SaveEdit);
            SaveBackCommand = new RelayCommand(SaveBack);
            BackCommand = new RelayCommand(Back);
        }

        private void Save()
        {
            using (var context = new Entities())
            {
                if (!SurveyVM.ValidateInputs()) return;

                SurveyVM.ToModel().OrderId = SurveyVM.OrderVM.Id;
                context.Surveys.Add(SurveyVM.ToModel());
                context.SaveChanges();
            }
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        private void SaveEdit()
        {
            Save();
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
            MessengerInstance.Send<ChangeSelectedSurveyMessage>(new ChangeSelectedSurveyMessage() { NextSurvey = _surveyVM });
        }

        private void SaveBack()
        {
            Save();
            Back();
        }
    }
}
