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
        public EventListVM EventList { get; set; }
        public EventVM Event { get; set; }
        public CustomerVM Customer {get; set; }
        public ICommand AddEventCommand { get; set; }
        public ICommand CloseAddEventCommand { get; set; }
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

        public AddEventVM(EventListVM eventList)
        {
            this.MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            {
                Customer = message.Customer;
                ContactPersons = message.Customer.ContactPersons;
                RaisePropertyChanged("ContactPersons");
                Event.Customer = Customer;
                Event.ContactPerson = ContactPersons.First();
                RaisePropertyChanged("Customer");
            });

            EventList = eventList;
            Event = new EventVM();
            AddEventCommand = new RelayCommand(AddEvent, CanAddEvent);
            CloseAddEventCommand = new RelayCommand(CloseAddEvent);
           
        }

        public void AddEvent()
        {
            var temp = Customer.Events;
            temp.Add(Event);
            Customer.Events = temp;

            using (var context = new Entities())
            {
                Event.CustomerModel = null;
                Event.ContactPersonModel = null;

                context.Events.Add(Event.ToModel());
                context.SaveChanges();
            }

            EventList.RefreshEvents();
            CloseAddEvent();
        }

        private void CloseAddEvent()
        {
            Event = new EventVM();
            Event.Customer = Customer;
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
