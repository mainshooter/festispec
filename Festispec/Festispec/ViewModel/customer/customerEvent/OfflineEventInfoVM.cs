using System.Windows.Input;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class OfflineEventInfoVM : ViewModelBase
    {
        private EventVM _event;

        public ICommand CloseSingleEventCommand { get; set; }

        public EventVM Event
        {
            get => _event;
            set
            {
                _event = value;
                RaisePropertyChanged(() => Event);
            }
        }

        public OfflineEventInfoVM()
        {
            CloseSingleEventCommand = new RelayCommand(CloseSingleEvent);
            MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                Event = message.Event;
            });
        }

        public void CloseSingleEvent()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(OfflineEventListPage) });
        }
    }
}
