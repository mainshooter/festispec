using Festispec.View.Report;
using Festispec.View.Report.Element;
using GalaSoft.MvvmLight;
using System.Windows.Controls;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Page Page { get; set; }
        public MainViewModel()
        {
            Page = new Report();
        }
    }
}