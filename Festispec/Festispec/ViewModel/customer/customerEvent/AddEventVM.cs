using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.ViewModel.customer.contactPerson;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class AddEventVM : ViewModelBase
    {
        private EventVM _eventVM;
        public EventListVM EventList { get; set; }
        public EventVM Event 
        { 
            get 
            {
                return _eventVM;
            }
            set {
                _eventVM = value;
                RaisePropertyChanged(() => Event);
            }
        }
        public ICommand AddEventCommand { get; set; }
        public ICommand CloseAddEventCommand { get; set; }
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

        public AddEventVM(EventListVM eventList)
        {
            this.MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            {
                ContactPersons = message.Customer.ContactPersons;
                RaisePropertyChanged("ContactPersons");
                Event.Customer = message.Customer;
                Event.ContactPerson = ContactPersons.First();
                RaisePropertyChanged("Event");
            });

            EventList = eventList;
            Event = new EventVM();
            AddEventCommand = new RelayCommand(AddEvent, CanAddEvent);
            CloseAddEventCommand = new RelayCommand(CloseAddEvent);
           
        }

        public void AddEvent()
        {
            var customer = Event.Customer;

            using (var context = new Entities())
            {
                Event.CustomerModel = null;
                Event.ContactPersonModel = null;

                context.Events.Add(Event.ToModel());
                context.SaveChanges();
            }

            Event.Customer = customer;
            var temp = Event.Customer.Events;
            temp.Add(Event);
            Event.Customer.Events = temp;

            EventList.RefreshEvents();
            CloseAddEvent();
        }

        private void CloseAddEvent()
        {
            var temp = Event.Customer;
            Event = new EventVM();
            Event.Customer = temp;
            Event.ContactPerson = ContactPersons.First();
            RaisePropertyChanged("Event");
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        public bool CanAddEvent()
        {
            return Event.IsValid;
        }
    }
}
