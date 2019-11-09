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

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public EmployeeListVM EmployeeList
        {
            get
            {
                return new EmployeeListVM(MainViewModel);
            }
        }

        public EmployeeVM Employee
        {
            get
            {
                return new EmployeeVM();
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
