using CommonServiceLocator;
using Festispec.ViewModel.auth;
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
        public UserLoginVm LoginVm => new UserLoginVm();
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}