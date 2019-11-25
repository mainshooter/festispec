using Festispec.Domain;
using Festispec.Message;
using Festispec.Repository;
using Festispec.View.Pages.Customer.Event;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        private EventVM _selectedEvent;

        public ICommand OpenAddEvent { get; set; }
        public ICommand OpenEditEvent { get; set; }
        public ICommand OpenSingleEvent { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ObservableCollection<EventVM> EventList { get; set; }
        public string SelectedFilter { get; set; }

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
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
                _filters.Add("Oppervlakte");
                _filters.Add("Klant");
            }
        }

        public ObservableCollection<EventVM> EventListFiltered
        {
            get
            {
                if (Filter != null || !Filter.Equals(""))
                {
                    switch (SelectedFilter)
                    {
                        case "Naam":
                            return new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.Name.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Begindatum":
                            return new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.BeginDate.ToString().Contains(Filter.ToLower())).ToList());
                        case "Bezoekersaantal":
                            return new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.AmountVisitors.ToString().Contains(Filter.ToLower())).ToList());
                        case "Oppervlakte":
                            return new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.SurfaceM2.ToString().Contains(Filter.ToLower())).ToList());
                        case "Klant":
                            return new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.Customer.Name.ToLower().Contains(Filter.ToLower())).ToList());
                    }
                }

                return EventList;
            }
        }

        public EventVM SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                if (value != null)
                {
                    _selectedEvent = value;
                    RaisePropertyChanged();
                }
            }
        }
        public EventListVM()
        {
            Filters = new List<string>();
            SelectedFilter = Filters.First();
            Filter = "";
            EventRepository eventRepository = new EventRepository();
            EventList = new ObservableCollection<EventVM>(eventRepository.GetEvents());
            OpenAddEvent = new RelayCommand(OpenAddEventPage);
            OpenEditEvent = new RelayCommand(OpenEditEventPage);
            DeleteEventCommand = new RelayCommand(DeleteEvent);
            //OpenSingleEvent = new RelayCommand(OpenSingleEventPage);
        }

        private void OpenAddEventPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddEventPage) });
        }

        private void OpenEditEventPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditEventPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = SelectedEvent,
                EventList = this
            });
        }

        public void DeleteEvent()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u dit evenement wilt verwijderen?", "Evenement Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    var temp = SelectedEvent.ToModel();
                    context.Events.Remove(context.Events.Select(eventcon => eventcon).Where(eventcon => eventcon.Id == temp.Id).First());
                    context.SaveChanges();
                }
                EventList.Remove(SelectedEvent);
                RaisePropertyChanged("EventListFiltered");
            }
        }

        public void RefreshEvents()
        {
            EventRepository eventRepository = new EventRepository();
            EventList = new ObservableCollection<EventVM>(eventRepository.GetEvents());
            RaisePropertyChanged("EventListFiltered");
        }
    }
}
