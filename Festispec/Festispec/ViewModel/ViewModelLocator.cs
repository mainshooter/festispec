using CommonServiceLocator;
using Festispec.ViewModel.planning;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<PlanningOverviewVM>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public PlanningOverviewVM PlanningOverviewVM => ServiceLocator.Current.GetInstance<PlanningOverviewVM>();


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}