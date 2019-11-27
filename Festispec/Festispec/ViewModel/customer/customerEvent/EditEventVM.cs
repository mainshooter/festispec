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
        private CustomerVM _customer;
        private int _contactIndex;
        public ICommand EditEventCommand { get; set; }
        public ICommand CloseEditEventCommand { get; set; }
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

        public CustomerVM Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                RaisePropertyChanged();
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
                Customer = message.Customer;
                Event = message.Event;
                ContactPersons = message.Customer.ContactPersons;
                ContactIndex = ContactPersons.IndexOf(ContactPersons.Select(contactPerson => contactPerson).Where(contactPerson => contactPerson.Id == Event.ContactPerson.Id).FirstOrDefault());
                RaisePropertyChanged("ContactPersons");

            });
            
            EditEventCommand = new RelayCommand(EditEvent, CanEditEvent);
            CloseEditEventCommand = new RelayCommand(CloseEditEvent);            

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
