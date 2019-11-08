using Festispec.Singleton;
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
        private PageSingleton _pagesingleton;

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
            _pagesingleton = new PageSingleton();

            Page = _pagesingleton.GetPage("dashboard");
        }

        //methodes
        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenDashboardTab()
        {
            Page = _pagesingleton.GetPage("dashboard");
        }

        private void OpenEmployeeTab()
        {
            Page = _pagesingleton.GetPage("employee");
        }

        private void OpenCustomerTab()
        {
            Page = _pagesingleton.GetPage("customer");
        }

        private void OpenAvailabilityTab()
        {
            Page = _pagesingleton.GetPage("availability");
        }

        private void OpenEventTab()
        {
            Page = _pagesingleton.GetPage("event");
        }

        private void OpenSickTab()
        {
            Page = _pagesingleton.GetPage("sick");
        }
    }
}