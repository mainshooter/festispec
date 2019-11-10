using Festispec.Singleton;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.employee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //privates
        private Page _page;
        private PageSingleton _pageSingleton;

        //publics
        public ICommand CloseApplication { get; set; }
        public ICommand OpenDashboard { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenCustomer { get; set; }
        public ICommand OpenAvailability { get; set; }
        public ICommand OpenEvent { get; set; }
        public ICommand OpenSick { get; set; }

        public Page Page
        {
            get { return _page; }
            set { _page = value; }
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
            _pageSingleton = new PageSingleton();

            Page = _pageSingleton.GetPage("dashboard");
            var employeeList = new EmployeePage();
            EmployeeListVM employeeListVM = new EmployeeListVM(this);
            employeeList.DataContext = employeeListVM;
            Page = employeeList;
        }

        //methodes
        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenDashboardTab()
        {
            Page = _pageSingleton.GetPage("dashboard");
        }

        private void OpenEmployeeTab()
        {
            Page = _pageSingleton.GetPage("employee");
        }

        private void OpenCustomerTab()
        {
            Page = _pageSingleton.GetPage("customer");
        }

        private void OpenAvailabilityTab()
        {
            Page = _pageSingleton.GetPage("availability");
        }

        private void OpenEventTab()
        {
            Page = _pageSingleton.GetPage("event");
        }

        private void OpenSickTab()
        {
            Page = _pageSingleton.GetPage("sick");
        }

        public void OpenAddEmployeeTab()
        {
            Page = _pageSingleton.GetPage("addemployee");
        }
    }
}
