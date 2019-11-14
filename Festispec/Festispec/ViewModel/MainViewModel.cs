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
            set { _page = value; RaisePropertyChanged("Page"); }
        }

        public PageSingleton PageSingleton
        {
            get { return _pageSingleton; }
            set { _pageSingleton = value; }

        }

        public EmployeeListVM EmployeeList { get; set; }

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

        public void OpenEmployeeTab()
        {
            var page = _pageSingleton.GetPage("employee"); ;
            EmployeeList = new EmployeeListVM(this);
            page.DataContext = EmployeeList;
            Page = page;
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
            var page = _pageSingleton.GetPage("addemployee");
            AddEmployeeVM addEmployeeVM = new AddEmployeeVM(EmployeeList);
            page.DataContext = addEmployeeVM;
            Page = page;
        }

        public void OpenEditEmployeeTab()
        {
            var page = _pageSingleton.GetPage("editemployee");
            EditEmployeeVM editEmployeeVM = new EditEmployeeVM(EmployeeList);
            page.DataContext = editEmployeeVM;
            Page = page;
        }

        public void OpenSingleEmployeeTab()
        {
            var page = _pageSingleton.GetPage("singleemployee");
            EmployeeVM EmployeeVM = new EmployeeVM(EmployeeList.SelectedEmployee.ToModel(), EmployeeList);
            page.DataContext = EmployeeVM;
            Page = page;
        }
    }
}
