using System.Windows.Controls;
using Festispec.View.Pages.survey;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Page _page;

        public Page Page
        {
            get { return _page; }
            set { _page = value; RaisePropertyChanged("Page"); }
        }

        public MainViewModel()
        {
            Page = new SurveyPage();
        }
    }
}