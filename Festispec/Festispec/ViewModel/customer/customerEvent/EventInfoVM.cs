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
        private CustomerVM _customer;

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

        public CustomerVM Customer 
        {
            get => _customer;
            set
            {
                _customer = value;
                RaisePropertyChanged();
            }
        }


        public EventInfoVM()
        {
            CloseSingleEventCommand = new RelayCommand(CloseSingleEvent);
            this.MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                Event = message.Event;
                Customer = message.Customer;
            });
        }

        public void CloseSingleEvent()
        {
            this.MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }
    }
}
