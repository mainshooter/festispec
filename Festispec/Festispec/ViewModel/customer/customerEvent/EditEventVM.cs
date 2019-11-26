using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.ViewModel.customer.contactPerson;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EditEventVM : ViewModelBase
    {
        public EventListVM EventList { get; set; }
        private EventVM _event;
        private int _customerIndex;
        private int _contactIndex;
        public ICommand EditEventCommand { get; set; }
        public ICommand CloseEditEventCommand { get; set; }
        public ObservableCollection<CustomerVM> Customers { get; set; }
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

        public ObservableCollection<ContactPersonVM> FilteredContactPersons
        {
            get
            {
                if (SelectedCustomer != null)
                {
                    return new ObservableCollection<ContactPersonVM>(ContactPersons.Select(contactperson => contactperson).Where(contactperson => contactperson.CustomerID == Event.Customer.Id));
                }
                return null;
            }
        }

        public CustomerVM SelectedCustomer
        {
            get
            {
                if(Event != null)
                {
                    return Event.Customer;
                }
                return null;
            }
            set
            {
                if (Event != null)
                {
                    Event.Customer = value;
                    RaisePropertyChanged("FilteredContactPersons");
                }      
            }
        }

        public EventVM Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                RaisePropertyChanged();
            }
        }

        public int CustomerIndex
        {
            get
            {
                return _customerIndex;
            }
            set
            {
                _customerIndex = value;
                RaisePropertyChanged();
            }
        }

        public int ContactIndex
        {
            get
            {
                return _contactIndex;
            }
            set
            {
                _contactIndex = value;
                RaisePropertyChanged();
            }
        }

        public EditEventVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                EventList = message.EventList;
                Event = message.Event;
                CustomerIndex = Customers.IndexOf(Customers.Select(customer => customer).Where(customer => customer.Id == Event.Customer.Id).FirstOrDefault());
                ContactIndex = ContactPersons.IndexOf(ContactPersons.Select(contactPerson => contactPerson).Where(contactPerson => contactPerson.Id == Event.ContactPerson.Id).FirstOrDefault());
            });
            
            EditEventCommand = new RelayCommand(EditEvent, CanEditEvent);
            CloseEditEventCommand = new RelayCommand(CloseEditEvent);

            using (var context = new Entities())
            {
                Customers = new ObservableCollection<CustomerVM>(context.Customers.ToList().Select(customer => new CustomerVM(customer)));
                ContactPersons = new ObservableCollection<ContactPersonVM>(context.ContactPersons.ToList().Select(contactPerson => new ContactPersonVM(contactPerson)));
            }

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(EventPage) && EventList != null)
                {
                    EventList.RefreshEvents();
                }
            });
        }

        public void EditEvent()
        {
            using (var context = new Entities())
            {
                Event.CustomerModel = null;
                Event.ContactPersonModel = null;

                context.Entry(Event.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            CloseEditEvent();
        }

        private void CloseEditEvent()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        public bool CanEditEvent()
        {
            if(Event == null)
            {
                return false;
            }
            return Event.IsValid;
        }
    }
}
