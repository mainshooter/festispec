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
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.Domain;
using System.Collections.Generic;
using Festispec.ViewModel.survey;
using System.Collections.ObjectModel;

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
        public ICommand OpenPlanning { get; set; }
        public ICommand OpenSurvey { get; set; }
        public ICommand ShowAccountInformation { get; set; }

        public ObservableCollection<Button> MenuList { get; set; }
        private Dictionary<string, Dictionary<string, ICommand>> Menu;

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
                CreateMenu();
                RaisePropertyChanged("LoggedInEmployee");
            }
        }

        //constructor
        public MainViewModel()
        {
            Menu = new Dictionary<string, Dictionary<string, ICommand>>();
            MenuList = new ObservableCollection<Button>();


            CloseApplication = new RelayCommand(CloseApp);
            OpenDashboard = new RelayCommand(OpenDashboardTab);
            OpenEmployee = new RelayCommand(OpenEmployeeTab);
            OpenCustomer = new RelayCommand(OpenCustomerTab);
            OpenEvent = new RelayCommand(OpenEventTab);
            OpenAvailability = new RelayCommand(OpenAvailabilityTab);
            OpenSick = new RelayCommand(OpenSickTab);
            OpenPlanning = new RelayCommand(OpenPlanningTab);
            OpenSurvey = new RelayCommand(OpenSurveyTab);
            ShowAccountInformation = new RelayCommand(OpenAccountInformation);

            Page = ServiceLocator.Current.GetInstance<LoginPage>();

            this.MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                this.Page = ServiceLocator.Current.GetInstance(message.NextPageType) as Page;
            });

            this.MessengerInstance.Register<ChangeLoggedinUserMessage>(this, message =>
            {
                LoggedInEmployee = message.LoggedinEmployee;
            });

            // Menu vullen
            FillMenuList();
            CreateMenu();
        }

        private void CreateMenu()
        {
            if(_loggedInEmployee == null)
            {
                return;
            }
            else
            {
                foreach(KeyValuePair<string, ICommand> entry in Menu[_loggedInEmployee.Department.Name])
                {
                    Button menuItem = new Button();
                    menuItem.Content = entry.Key;
                    menuItem.Command = entry.Value;
                    menuItem.Width = 150;
                    menuItem.Background = null;
                    menuItem.BorderBrush = null;
                    menuItem.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    MenuList.Add(menuItem);
                }
            }
            RaisePropertyChanged("MenuList");
        }

        private void FillMenuList()
        {
            // Inspectie Dictionary
            Menu.Add("Inspectie", new Dictionary<string, ICommand>());
            Menu["Inspectie"].Add("Dashboard", OpenDashboard);
            Menu["Inspectie"].Add("Beschikbaarheid", OpenAvailability);
            Menu["Inspectie"].Add("Ziek melden", OpenSick);

            // Sales Dictionary
            Menu.Add("Sales", new Dictionary<string, ICommand>());
            Menu["Sales"].Add("Dashboard", OpenDashboard);
            Menu["Sales"].Add("Klanten", OpenCustomer);

            // Planning Dictionary
            Menu.Add("Planning", new Dictionary<string, ICommand>());
            Menu["Planning"].Add("Dashboard", OpenDashboard);
            Menu["Planning"].Add("Planning", OpenPlanning);

            // Directie Dictionary
            Menu.Add("Directie", new Dictionary<string, ICommand>());
            Menu["Directie"].Add("Dashboard", OpenDashboard);
            Menu["Directie"].Add("Werknemers", OpenEmployee);
            Menu["Directie"].Add("Beschikbaarheid", OpenAvailability);
            Menu["Directie"].Add("Ziek melden", OpenSick);
            Menu["Directie"].Add("Evenementen", OpenEvent);
            Menu["Directie"].Add("Klanten", OpenCustomer);
            Menu["Directie"].Add("Vragenlijsten", OpenSurvey);
            Menu["Directie"].Add("Planning", OpenPlanning);


            // Marketing Dictionary
            Menu.Add("Marketing", new Dictionary<string, ICommand>());
            Menu["Marketing"].Add("Dashboard", OpenDashboard);
            Menu["Marketing"].Add("Werknemers", OpenEmployee);
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

        private void OpenPlanningTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }

        //Voorbeeld van specifieke event
        private void OpenSpecificPlanningTab()
        {
            using (var context = new Entities())
            {
                List<Order> order = context.Orders.ToList();
                OrderVM orderVM = new OrderVM(order.FirstOrDefault());
                EventVM eventVM = orderVM.Event;
                eventVM.OrderVM = orderVM;
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
                MessengerInstance.Send<ChangeSelectedEventVM>(new ChangeSelectedEventVM() { NextEvent = eventVM });
            }
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

        private void OpenAccountInformation()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeeInformationPage) });
        }
    }
}
