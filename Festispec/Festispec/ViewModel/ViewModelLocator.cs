using CommonServiceLocator;
using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Employee;
using Festispec.View.Pages.Employee.Availability;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.employee.availabilty;
using Festispec.ViewModel.report;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<DashboardPage>();
            SimpleIoc.Default.Register<ReportPage>();
            SimpleIoc.Default.Register<CustomerPage>();
            SimpleIoc.Default.Register<EventPage>();
            SimpleIoc.Default.Register<AvailablePage>();
            SimpleIoc.Default.Register<EmployeePage>();
            SimpleIoc.Default.Register<SickPage>();
            SimpleIoc.Default.Register<AddElementPage>();
            SimpleIoc.Default.Register<EmployeePage>();
            SimpleIoc.Default.Register<AddEmployeePage>();
            SimpleIoc.Default.Register<SingleEmployeePage>();
            SimpleIoc.Default.Register<EditEmployeePage>();
            SimpleIoc.Default.Register<LoginPage>();

            SimpleIoc.Default.Register<ReportVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EmployeeVM>();
            SimpleIoc.Default.Register<EmployeeListVM>();
            SimpleIoc.Default.Register<AddEmployeeVM>();
            SimpleIoc.Default.Register<AddElementVM>();
            SimpleIoc.Default.Register<EmployeeInfoVM>();
            SimpleIoc.Default.Register<EditEmployeeVM>();
            SimpleIoc.Default.Register<UserLoginVM>();

        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public ReportVM ReportVM => ServiceLocator.Current.GetInstance<ReportVM>();

        public UserLoginVM UserLoginVM => ServiceLocator.Current.GetInstance<UserLoginVM>();

        public AddEmployeeVM AddEmployeeVM => ServiceLocator.Current.GetInstance<AddEmployeeVM>();

        public EmployeeListVM EmployeeListVM => ServiceLocator.Current.GetInstance<EmployeeListVM>();

        public EmployeeInfoVM EmployeeInfoVM => ServiceLocator.Current.GetInstance<EmployeeInfoVM>();

        public EditEmployeeVM EditEmployeeVM => ServiceLocator.Current.GetInstance<EditEmployeeVM>();
        public AvailabilityManagerVM AvailabilityManagerVM => ServiceLocator.Current.GetInstance<AvailabilityManagerVM>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
