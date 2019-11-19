using System.Linq;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.employee;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using CommonServiceLocator;
using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Employee.Availability;
using Festispec.View.Pages.Customer.Event;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.Domain;
using Festispec.View.Pages.Employee;
using Festispec.View.Pages.Survey;
using Festispec.ViewModel.survey;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //privates
        private Page _page;
        private EmployeeVM _loggedInEmployee;

        //publics
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
            get => _page;
            set { _page = value; RaisePropertyChanged("Page"); }
        }

        public EmployeeVM LoggedInEmployee {
            get {
                return _loggedInEmployee;
            }
            set {
                _loggedInEmployee = value;
                RaisePropertyChanged("LoggedInEmployee");
            }
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

            Page = ServiceLocator.Current.GetInstance<LoginPage>();

            this.MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                this.Page = ServiceLocator.Current.GetInstance(message.NextPageType) as Page;
            });
            this.MessengerInstance.Register<ChangeLoggedinUserMessage>(this, message =>
            {
                LoggedInEmployee = message.LoggedinEmployee;
            });
        }


        //methodes
        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenDashboardTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(DashboardPage) });
        }

        public void OpenEmployeeTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePage) });
        }

        private void OpenCustomerTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerPage) });
        }

        private void OpenAvailabilityTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AvailablePage) });
        }

        private void OpenEventTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        private void OpenSickTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SickPage)});
        }

        private void OpenSurveyTab()
        {
            using (var context = new Entities())
            {
                var surveyDomain = context.Surveys.First();
                var survey = new SurveyVM(surveyDomain);
                
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage)});
                MessengerInstance.Send<ChangeSelectedSurveyMessage>(new ChangeSelectedSurveyMessage() { NextSurvey = survey });
            }
        }
    }
}
