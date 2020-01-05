using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.ViewModel.customer.contactPerson;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EditEventVM : ViewModelBase
    {
        private EventVM _event;
        private int _contactIndex;

        public ICommand EditEventCommand { get; set; }
        public ICommand CloseEditEventCommand { get; set; }
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }
        public EventListVM EventList { get; set; }

        public EventVM Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                RaisePropertyChanged("Event");
                RaisePropertyChanged("CanEditDate");
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
                RaisePropertyChanged("ContactIndex");
            }
        }

        public EditEventVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                EventList = message.EventList;
                Event = message.Event;
                ContactPersons = Event.Customer.ContactPersons;
                ContactIndex = ContactPersons.IndexOf(ContactPersons.Select(contactPerson => contactPerson).Where(contactPerson => contactPerson.Id == Event.ContactPerson.Id).FirstOrDefault());
                RaisePropertyChanged("ContactPersons");
            });
            
            EditEventCommand = new RelayCommand(EditEvent, CanEditEvent);
            CloseEditEventCommand = new RelayCommand(CloseEditEvent);            
        }

        public void EditEvent()
        {
            Event eventCon = Event.ToModel();
            ICollection<Order> order = eventCon.Orders;
            eventCon.Orders = null;

            using (var context = new Entities())
            {
                Event.CustomerModel = null;
                Event.ContactPersonModel = null;

                context.Entry(eventCon).State = EntityState.Modified;
                context.SaveChanges();
            }
            eventCon.Orders = order;
            CloseEditEvent();
        }

        private void CloseEditEvent()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        public bool CanEditDate
        {
            get
            {
                if(Event == null || Event.OrderVM != null)
                {
                    return false;
                }
                return true;
            }
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
