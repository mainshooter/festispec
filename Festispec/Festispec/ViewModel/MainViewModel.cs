using System.Windows;
using System.Windows.Media.Animation;
using Festispec.View;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            var login = new LoginWindow();
            login.Show();
        }
    }
}