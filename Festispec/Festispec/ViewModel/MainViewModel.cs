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
using Festispec.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;
using Hanssens.Net;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //privates
        private Page _page;
        private EmployeeVM _loggedInEmployee;
        private Dictionary<string,Dictionary<string,ICommand>> _menu;

        //publics
        public ICommand CloseApplication { get; set; }
        public ICommand OpenDashboard { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenCustomer { get; set; }
        public ICommand OpenAvailability { get; set; }
        public ICommand OpenEvent { get; set; }
        public ICommand OpenSick { get; set; }
        public ICommand ShowAccountInformation { get; set; }
        public ObservableCollection<Button> MenuList { get; set; }

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
            OpenEvent = new RelayCommand(OpenEventTab);
            OpenAvailability = new RelayCommand(OpenAvailabilityTab);
            OpenSick = new RelayCommand(OpenSickTab);
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
            TestLocalStorage();
        }

        private void CreateMenu()
        {
            if(_loggedInEmployee == null)
            {
                return;
            }
            else
            {
                foreach(KeyValuePair<string, ICommand> entry in _menu[_loggedInEmployee.Department.Name])
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
            _menu["Inspectie"].Add("Ziek melden", OpenSick);

            // Sales Dictionary
            _menu.Add("Sales", new Dictionary<string, ICommand>());
            _menu["Sales"].Add("Dashboard", OpenDashboard);
            _menu["Sales"].Add("Klanten", OpenCustomer);

            // Planning Dictionary
            _menu.Add("Planning", new Dictionary<string, ICommand>());
            _menu["Planning"].Add("Dashboard", OpenDashboard);

            // Directie Dictionary
            _menu.Add("Directie", new Dictionary<string, ICommand>());
            _menu["Directie"].Add("Dashboard", OpenDashboard);
            _menu["Directie"].Add("Werknemers", OpenEmployee);
            _menu["Directie"].Add("Ziek melden", OpenSick);
            _menu["Directie"].Add("Evenementen", OpenEvent);
            _menu["Directie"].Add("Beschikbaarheid", OpenAvailability);
            _menu["Directie"].Add("Klanten", OpenCustomer);

            // Marketing Dictionary
            _menu.Add("Marketing", new Dictionary<string, ICommand>());
            _menu["Marketing"].Add("Dashboard", OpenDashboard);
            _menu["Marketing"].Add("Werknemers", OpenEmployee);
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
            using (var context = new Entities())
            {
                var customerDomain = context.Customers.First();
                var customer = new CustomerVM(customerDomain);

                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
                MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage() { Customer = customer });
            }
        }

        private void OpenSickTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SickPage)});
        }

        private void OpenAccountInformation()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeeInformationPage) });
        }

        private void TestLocalStorage()
        {
            using (var storage = new LocalStorage())
            {
                List<EventVM> events = new List<EventVM>();

                foreach (var key in storage.Keys())
                {
                    var storageObject = storage.Get<EventVM>(key);
                    events.Add(storageObject);
                }
            }
        }
    }
}
