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

        public bool CanEditDate
        {
            get
            {
                if (Event == null) return false;
                if (Event.OrderVM == null) return true;
                if (Event.OrderVM.Id == 0) return true;
                return false;
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
                var eventModel = context.Events.Find(Event.Id);
                eventModel.Name = Event.Name;
                eventModel.PostalCode = Event.PostalCode;
                eventModel.Street = Event.Street;
                eventModel.SurfaceM2 = Event.SurfaceM2;
                eventModel.AmountVisitors = Event.AmountVisitors;
                eventModel.BeginDate = Event.BeginDate;
                eventModel.City = Event.City;
                eventModel.ContactPersonId = Event.ContactPerson.Id;
                eventModel.Description = Event.Description;
                eventModel.EndDate = Event.EndDate;
                eventModel.HouseNumber = Event.HouseNumber;

                context.Entry(eventModel).State = EntityState.Modified;
                context.SaveChanges();
            }
            eventCon.Orders = order;
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
