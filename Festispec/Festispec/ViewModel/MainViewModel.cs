using Festispec.Singleton;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.survey;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //privates
        private Page _page;

        //publics
        public PageSingleton PageSingleton { get; set; }
        public ICommand CloseApplication { get; set; }
        public ICommand OpenDashboard { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenCustomer { get; set; }
        public ICommand OpenAvailability { get; set; }
        public ICommand OpenEvent { get; set; }
        public ICommand OpenSick { get; set; }
        public ICommand OpenSurvey { get; set; }

        public Page Page
        {
            get { return _page; }
            set { _page = value; RaisePropertyChanged("Page"); }
        }

        //constructor
        public MainViewModel()
        {
            CloseApplication = new RelayCommand(CloseApp);
            OpenDashboard = new RelayCommand(OpenDashboardTab);
            OpenEmployee = new RelayCommand(OpenEmployeeTab);
            OpenCustomer = new RelayCommand(OpenCustomerTab);
            OpenAvailability = new RelayCommand(OpenAvailabilityTab);
            OpenEvent = new RelayCommand(OpenEventTab);
            OpenSick = new RelayCommand(OpenSickTab);
            OpenSurvey = new RelayCommand(OpenSurveyTab);
            PageSingleton = new PageSingleton(this);

            Page = PageSingleton.GetPage("dashboard");
        }

        //methodes
        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenDashboardTab()
        {
            Page = PageSingleton.GetPage("dashboard");
        }

        private void OpenEmployeeTab()
        {
            Page = PageSingleton.GetPage("employee");
        }

        private void OpenCustomerTab()
        {
            Page = PageSingleton.GetPage("customer");
        }

        private void OpenAvailabilityTab()
        {
            Page = PageSingleton.GetPage("availability");
        }

        private void OpenEventTab()
        {
            Page = PageSingleton.GetPage("event");
        }

        private void OpenSickTab()
        {
            Page = PageSingleton.GetPage("sick");
        }

        private void OpenSurveyTab()
        {
            Page = PageSingleton.GetPage("survey");
            var survey = new Survey();
            survey.Questions.Add(new Question(){
                Type = "Open vraag",
                Question1 = "{\r\n    \"Question\"    : \"Bla bla\",\r\n    \"Choices\":\r\n        {\r\n            \"Cols\": [\"Act\",\"Sfeer\"],\r\n            \"Options\": [\"Grimmig\",\"Donken\"]\r\n        },\r\n    \"Description\" : \"Bla bla\",\r\n    \"Images\"      : []\r\n}",
                Order = 1
            });
            survey.Questions.Add(new Question()
            {
                Type = "Gesloten vraag",
                Question1 = "{\r\n    \"Question\"    : \"Bla bla\",\r\n    \"Choices\":\r\n        {\r\n            \"Cols\": [],\r\n            \"Options\": []\r\n        },\r\n    \"Description\" : \"bla bla\",\r\n    \"Images\"      : [\"234trgderty56et44567irte\", \"234trgderty56et44567irte\"]\r\n}",
                Order = 2
            });
            var selectedEvent = new EventVM {Name = "Pinkpop"};
            Page.DataContext = new SurveyVM(this, selectedEvent, survey);
        }
    }
}