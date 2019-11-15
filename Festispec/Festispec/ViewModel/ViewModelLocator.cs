using CommonServiceLocator;
using Festispec.Singleton;
using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Report;
using Festispec.ViewModel.report;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<PageSingleton>();

            SimpleIoc.Default.Register<DashboardPage>();
            SimpleIoc.Default.Register<ReportPage>();
            SimpleIoc.Default.Register<CustomerPage>();
            SimpleIoc.Default.Register<EventPage>();
            SimpleIoc.Default.Register<AvailablePage>();

            SimpleIoc.Default.Register<ReportVM>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ReportVM ReportVM => ServiceLocator.Current.GetInstance<ReportVM>();


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
