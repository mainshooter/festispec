using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.customerEvent
{
    class AddEventVM : ViewModelBase
    {
        public EventVM Event { get; set; }
        public ICommand AddEventCommand { get; set; }

        public AddEventVM()
        {
            Event = new EventVM();
            AddEventCommand = new RelayCommand(AddEvent, CanAddEvent);
        }

        public void AddEvent()
        {

        }

        public bool CanAddEvent()
        {
            return false;
        }
    }
}
