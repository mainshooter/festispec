using CommonServiceLocator;
using Festispec.ViewModel.employee;
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
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AddEmployeeVM AddEmployee
        {
            get
            {
                return new AddEmployeeVM();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}