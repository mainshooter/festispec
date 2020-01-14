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
using Festispec.Message;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Festispec.View.Pages.Planning;
using Festispec.View.Pages.Employee.Planning;
using Festispec.View.Pages.Map;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //privates
        private Page _page;
        private EmployeeVM _loggedInEmployee;
        private Dictionary<string, Dictionary<string, ICommand>> _menu;
        private string _rightTopMenuVisablity;

        //publics
        public ICommand CloseApplication { get; set; }
        public ICommand OpenDashboard { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenCustomer { get; set; }
        public ICommand OpenAvailability { get; set; }
        public ICommand OpenSick { get; set; }
        public ICommand OpenWorkedHours { get; set; }
        public ICommand ShowAccountInformation { get; set; }
        public ICommand OpenEmployeePlanningCommand { get; set; }
        public ICommand OpenMapCommand { get; set; }
        public ObservableCollection<Button> MenuList { get; set; }

        public string RightTopMenuVisablity
        { 
            get => _rightTopMenuVisablity;
            set
            {
                _rightTopMenuVisablity = value;
                RaisePropertyChanged("RightTopMenuVisablity");
            }
        }

        public Page Page
        {
            get => _page;
            set
            {
                _page = value;
                RaisePropertyChanged("Page");
            }
        }

        public EmployeeVM LoggedInEmployee
        {
            get => _loggedInEmployee;
            set
            {
                _loggedInEmployee = value;
                CreateMenu();
                RaisePropertyChanged("LoggedInEmployee");
            }
        }

        //constructor
        public MainViewModel()
        {
            _menu = new Dictionary<string, Dictionary<string, ICommand>>();
            MenuList = new ObservableCollection<Button>();
            CloseApplication = new RelayCommand(CloseApp);
            OpenDashboard = new RelayCommand(OpenDashboardTab);
            OpenEmployee = new RelayCommand(OpenEmployeeTab);
            OpenCustomer = new RelayCommand(OpenCustomerTab);
            OpenAvailability = new RelayCommand(OpenAvailabilityTab);
            OpenSick = new RelayCommand(OpenSickTab);
            OpenWorkedHours = new RelayCommand(OpenWorkedHoursTab);
            ShowAccountInformation = new RelayCommand(OpenAccountInformation);
            OpenEmployeePlanningCommand = new RelayCommand(OpenEmployeePlanning);
            OpenMapCommand = new RelayCommand(OpenMap);

            this.MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                this.Page = ServiceLocator.Current.GetInstance(message.NextPageType) as Page;

                if (message.NextPageType == typeof(LoginPage))
                {
                    LoggedInEmployee = null;
                }
            });

            this.MessengerInstance.Register<ChangeLoggedinUserMessage>(this, message =>
            {
                LoggedInEmployee = message.LoggedinEmployee;
            });
                
            // Menu vullen
            FillMenuList();
            CreateMenu();
            RightTopMenuVisablity = "Collapsed";
            Page = ServiceLocator.Current.GetInstance<LoginPage>();
        }

        private void CreateMenu()
        {
            MenuList.Clear();
            if (_loggedInEmployee == null)
            {
                return;
            }
            else
            {
                foreach (KeyValuePair<string, ICommand> entry in _menu[_loggedInEmployee.Department.Name])
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
            _menu.Add("Inspectie", new Dictionary<string, ICommand>());
            _menu["Inspectie"].Add("Dashboard", OpenDashboard);
            _menu["Inspectie"].Add("Beschikbaarheid", OpenAvailability);
            _menu["Inspectie"].Add("Ingeplande dagen", OpenEmployeePlanningCommand);
            _menu["Inspectie"].Add("Ziekmelden", OpenSick);
            _menu["Inspectie"].Add("Urenregistratie", OpenWorkedHours);
            _menu["Inspectie"].Add("Kaart", OpenMapCommand);

            // Sales Dictionary
            _menu.Add("Sales", new Dictionary<string, ICommand>());
            _menu["Sales"].Add("Dashboard", OpenDashboard);
            _menu["Sales"].Add("Klanten", OpenCustomer);
            _menu["Sales"].Add("Kaart", OpenMapCommand);

            // Planning Dictionary
            _menu.Add("Planning", new Dictionary<string, ICommand>());
            _menu["Planning"].Add("Dashboard", OpenDashboard);
            _menu["Planning"].Add("Klanten", OpenCustomer);
            _menu["Planning"].Add("Kaart", OpenMapCommand);

            // Directie Dictionary
            _menu.Add("Directie", new Dictionary<string, ICommand>());
            _menu["Directie"].Add("Dashboard", OpenDashboard);
            _menu["Directie"].Add("Klanten", OpenCustomer);
            _menu["Directie"].Add("Werknemers", OpenEmployee);
            _menu["Directie"].Add("Kaart", OpenMapCommand);

            // Marketing Dictionary
            _menu.Add("Marketing", new Dictionary<string, ICommand>());
            _menu["Marketing"].Add("Dashboard", OpenDashboard);
            _menu["Marketing"].Add("Werknemers", OpenEmployee);
            _menu["Marketing"].Add("Klanten", OpenCustomer);
            _menu["Marketing"].Add("Kaart", OpenMapCommand);
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
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerOverviewPage) });
        }

        private void OpenAvailabilityTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AvailablePage) });
        }

        private void OpenWorkedHoursTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(WorkedPlannedEmployeePage) });
            MessengerInstance.Send<ChangeSelectedEmployeeMessage>(new ChangeSelectedEmployeeMessage() { Employee = LoggedInEmployee });
        }
        private void OpenEmployeePlanning()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePlanningPage)} );
        }

        private void OpenSickTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SickPage) });
        }

        private void OpenAccountInformation()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeeInformationPage) });
        }

        private void OpenMap()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(MapPage) });
        }
    }
}
