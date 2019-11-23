using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.ViewModel.customer.contactPerson;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
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
        public ObservableCollection<CustomerVM> Customers {get; set;}
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

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
        }

        public void AddEvent()
        {

            using (var context = new Entities())
            {
                Event.ContactPerson = null;
                Event.Customer = null;
                context.Events.Add(Event.ToModel());
                context.SaveChanges();
            }

            EventList.EventList.Add(Event);
            EventList.RaisePropertyChanged("EventListFiltered");
            CloseAddEvent();
        }

        private void CloseAddEvent()
        {
            Event = new EventVM();
            RaisePropertyChanged("Event");
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        public bool CanAddEvent()
        {
            if (Event.Customer == null || Event.ContactPerson == null || Event.Name == null || Event.Street == null || Event.HouseNumber == 0 || Event.PostalCode == null || Event.City == null || Event.BeginDate == null || Event.EndDate == null || Event.SurfaceM2 == 0)
            {
                return false;
            }

            if (Event.HouseNumberAddition != null)
            {
                if (Event.HouseNumberAddition.Length > 5)
                {
                    return false;
                }
            }

            if (Event.BeginDate < DateTime.Today || Event.EndDate < Event.BeginDate || Event.Name.Length > 100 || Event.Street.Length > 100 || Event.PostalCode.Length > 6 || Event.City.Length > 45)
            {
                return false;
            }

            return true;

        }
    }
}
