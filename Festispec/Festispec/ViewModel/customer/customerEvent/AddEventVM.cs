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
        public ICommand AddEventCommand { get; set; }
        public ICommand CloseAddEventCommand { get; set; }
        private CustomerVM _selectedCustomer;
        public ObservableCollection<CustomerVM> Customers {get; set;}
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }
        
        public ObservableCollection<ContactPersonVM> FilteredContactPersons
        {
            get
            {
                if(SelectedCustomer != null)
                {
                    return new ObservableCollection<ContactPersonVM>(ContactPersons.Select(contactperson => contactperson).Where(contactperson => contactperson.CustomerID == _selectedCustomer.Id));
                }
                return null;
            }
        }

        public CustomerVM SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                Event.Customer = value;
                _selectedCustomer = value;
                RaisePropertyChanged("FilteredContactPersons");
            }
        }

        public AddEventVM(EventListVM eventList)
        {
            EventList = eventList;
            Event = new EventVM();
            AddEventCommand = new RelayCommand(AddEvent, CanAddEvent);
            CloseAddEventCommand = new RelayCommand(CloseAddEvent);

            using (var context = new Entities())
            {
                Customers = new ObservableCollection<CustomerVM>(context.Customers.ToList().Select(customer => new CustomerVM(customer)));
                ContactPersons = new ObservableCollection<ContactPersonVM>(context.ContactPersons.ToList().Select(contactPerson => new ContactPersonVM(contactPerson)));
            }

            Event.Customer = Customers.First();
            Event.ContactPerson = ContactPersons.Select(contactPerson => contactPerson).Where(contactPerson => contactPerson.CustomerID == Event.Customer.Id).First();
        }

        public void AddEvent()
        {
            EventList.EventList.Add(Event);

            using (var context = new Entities())
            {
                Event.CustomerModel = null;
                Event.ContactPersonModel = null;

                context.Events.Add(Event.ToModel());
                context.SaveChanges();
            }

            EventList.RaisePropertyChanged("EventListFiltered");
            CloseAddEvent();
        }

        private void CloseAddEvent()
        {
            Event = new EventVM();
            Event.Customer = Customers.First();
            Event.ContactPerson = ContactPersons.Select(contactPerson => contactPerson).Where(contactPerson => contactPerson.CustomerID == Event.Customer.Id).First();
            RaisePropertyChanged("Event");
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        public bool CanAddEvent()
        {
            return Event.IsValid;
        }
    }
}
