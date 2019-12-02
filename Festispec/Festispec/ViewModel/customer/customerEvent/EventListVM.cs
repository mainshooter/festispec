using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EventListVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filters;
        private bool _showOnlyFuture;

        public CustomerVM Customer { get; set; }
        public ICommand OpenAddEvent { get; set; }
        public ICommand OpenEditEvent { get; set; }
        public ICommand OpenSingleEvent { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ObservableCollection<EventVM> EventList { get; set; }
        public string SelectedFilter { get; set; }

        public string Title
        {
            get
            {
                if (Customer != null)
                {
                    return "Evenementen " + Customer.Name;
                }
                return null;
            }
        }

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                RaisePropertyChanged("EventListFiltered");
            }
        }

        public bool ShowOnlyFuture
        {
            get
            {
                return _showOnlyFuture;
            }
            set
            {
                _showOnlyFuture = value;
                RaisePropertyChanged("EventListFiltered");
            }
        }

        public List<string> Filters
        {
            get => _filters;
            set
            {
                _filters = new List<string>();
                _filters.Add("Naam");
                _filters.Add("Begindatum");
                _filters.Add("Bezoekersaantal");
                _filters.Add("Contactpersoon");
            }
        }

        public ObservableCollection<EventVM> EventListFiltered
        {
            get
            {
                var temp = new ObservableCollection<EventVM>();

                if (EventList == null)
                {
                    return temp;
                }

                if (Filter != null || !Filter.Equals(""))
                {
                    switch (SelectedFilter)
                    {
                        case "Naam":
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.Name.ToLower().Contains(Filter.ToLower())).ToList());
                            break;
                        case "Begindatum":
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.BeginDate.ToString().Contains(Filter.ToLower())).ToList());
                            break;
                        case "Bezoekersaantal":
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.AmountVisitors.ToString().Contains(Filter.ToLower())).ToList());
                            break;
                        case "Contactpersoon":
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.ContactPerson.Fullname.ToLower().Contains(Filter.ToLower())).ToList());
                            break;
                    }

                    if (_showOnlyFuture)
                    {
                        temp = new ObservableCollection<EventVM>(temp.ToList().Where(i => i.EndDate >= DateTime.Today).ToList());
                    }
                }

                return temp;
            }
        }

        public EventListVM()
        {
            this.MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            {
                Customer = message.Customer;
                EventList = message.Customer.Events;
                RaisePropertyChanged("EventListFiltered");
                RaisePropertyChanged("Title");
            });

            Filters = new List<string>();
            SelectedFilter = Filters.First();
            Filter = "";
            ShowOnlyFuture = true;
            OpenAddEvent = new RelayCommand(OpenAddEventPage);
            OpenEditEvent = new RelayCommand<EventVM>(OpenEditEventPage, CanOpenEdit);
            DeleteEventCommand = new RelayCommand<EventVM>(DeleteEvent, CanOpenDelete);
            OpenSingleEvent = new RelayCommand<EventVM>(OpenSingleEventPage);

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(EventPage) && EventList != null)
                {
                    this.RefreshEvents();
                }
            });
        }

        private void OpenAddEventPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddEventPage) });
            MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage()
            {
                Customer = Customer
            });
        }

        private void OpenEditEventPage(EventVM source)
        {

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditEventPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = source,
                EventList = this,
            });
        }

        private bool CanOpenEdit(EventVM source)
        {
           
            return source != null && source.EndDate >= DateTime.Today;
        }

        public void DeleteEvent(EventVM source)
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u dit evenement wilt verwijderen?", "Evenement Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    var temp = source.ToModel();
                    context.Events.Remove(context.Events.Select(eventcon => eventcon).Where(eventcon => eventcon.Id == temp.Id).First());
                    context.SaveChanges();
                }
                EventList.Remove(source);
                RaisePropertyChanged("EventListFiltered");
            }
        }

        private bool CanOpenDelete(EventVM source)
        {
            if (source == null) return false;
            if (source.ToModel().Orders.Count() > 0) return false;
            if (source.EndDate < DateTime.Today) return false;
            return true;
        }

        public void OpenSingleEventPage(EventVM source)
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SingleEventPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = source,
            }) ;
        }

        public void RefreshEvents()
        {
            EventList = Customer.Events;
            RaisePropertyChanged("EventListFiltered");
        }
    }
}
