using CommonServiceLocator;
using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Employee;
using Festispec.View.Pages.Employee.Availability;
using Festispec.View.Pages.Planning;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.planning;
using Festispec.ViewModel.report;
using Festispec.ViewModel.toast;
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
            SimpleIoc.Default.Register<PlanningOverviewPage>();

            SimpleIoc.Default.Register<ReportVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddElementVM>();
            SimpleIoc.Default.Register<ToastVM>();
            SimpleIoc.Default.Register<PlanningOverviewVM>();

        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ReportVM ReportVM => ServiceLocator.Current.GetInstance<ReportVM>();

        public PlanningOverviewVM PlanningOverviewVM => ServiceLocator.Current.GetInstance<PlanningOverviewVM>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
