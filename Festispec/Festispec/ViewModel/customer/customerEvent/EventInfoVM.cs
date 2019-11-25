using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EventInfoVM : ViewModelBase
    {
        private EventVM _event;

        public ICommand CloseSingleEventCommand { get; set; }

        public EventVM Event
        {
            get => _event;
            set
            {
                _event = value;
                RaisePropertyChanged();
            }
        }

        public EventInfoVM()
        {
            CloseSingleEventCommand = new RelayCommand(CloseSingleEvent);
            this.MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                Event = message.Event;
            });
        }

        public void CloseSingleEvent()
        {
            this.MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }
    }
}
