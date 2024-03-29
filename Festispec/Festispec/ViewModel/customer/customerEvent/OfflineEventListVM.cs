﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hanssens.Net;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class OfflineEventListVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filters;
        private bool _showOnlyFuture;

        public CustomerVM Customer { get; set; }
        public ICommand OpenSingleEventCommand { get; set; }
        public ICommand BackCommand { get; set; }
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

        public bool ShowOnlyFuture
        {
            get => _showOnlyFuture;
            set
            {
                _showOnlyFuture = value;
                RaisePropertyChanged("EventListFiltered");
            }
        }

        public List<string> Filters
        {
            get => _filters;
            set => _filters = new List<string> {"Naam", "Begindatum", "Bezoekersaantal", "Contactpersoon"};
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
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.Name.ToLower().Contains(Filter.ToLower())).OrderBy(e => e.BeginDate).ToList());
                            break;
                        case "Begindatum":
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.BeginDate.ToString().Contains(Filter.ToLower())).OrderBy(e => e.BeginDate).ToList());
                            break;
                        case "Bezoekersaantal":
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.AmountVisitors.ToString().Contains(Filter.ToLower())).OrderBy(e => e.BeginDate).ToList());
                            break;
                        case "Contactpersoon":
                            temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.ContactPerson.Fullname.ToLower().Contains(Filter.ToLower())).OrderBy(e => e.BeginDate).ToList());
                            break;
                    }

                    if (_showOnlyFuture)
                    {
                        temp = new ObservableCollection<EventVM>(temp.ToList().Where(i => i.EndDate >= DateTime.Today).OrderBy(e => e.BeginDate).ToList());
                    }
                }

                return temp;
            }
        }

        public OfflineEventListVM()
        {
            Filters = new List<string>();
            SelectedFilter = Filters.First();
            Filter = "";
            ShowOnlyFuture = true;
            OpenSingleEventCommand = new RelayCommand<EventVM>(OpenSingleEventPage);
            BackCommand = new RelayCommand(Back);
            DeleteEventCommand = new RelayCommand<EventVM>(DeleteEvent);
            RefreshEvents();

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(OfflineEventListPage))
                {
                    RefreshEvents();
                }
            });
        }

        public void OpenSingleEventPage(EventVM source)
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SingleOfflineEventPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = source
            });
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(LoginPage) });
        }

        private void RefreshEvents()
        {
            using (var storage = new LocalStorage())
            {
                var events = new ObservableCollection<EventVM>();

                foreach (var key in storage.Keys())
                {
                    var storageObject = storage.Get<EventVM>(key);
                    storageObject.ToModel().Id = Convert.ToInt32(key);
                    events.Add(storageObject);
                }

                EventList = events;
                RaisePropertyChanged(() => EventListFiltered);
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowInformation( events.Count + " offline evenement gevonden.");
            }
        }

        private void DeleteEvent(EventVM eventVM)
        {
            using (var storage = new LocalStorage())
            {
                storage.Dispose();
                storage.Clear();

                foreach (var e in EventList.ToList())
                {
                    if (eventVM.Id == e.Id)
                    {
                        EventList.Remove(e);
                    }
                    else
                    {
                        storage.Store(e.Id.ToString(), e);
                    }
                }

                storage.Persist();
                RaisePropertyChanged(() => EventListFiltered);
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowInformation("Evenement " + eventVM.Name + " niet meer offline beschikbaar.");
            }
        }
    }
}
