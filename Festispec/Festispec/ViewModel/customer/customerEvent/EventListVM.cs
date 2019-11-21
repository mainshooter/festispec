﻿using Festispec.Message;
using Festispec.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EventListVM
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
                //RaisePropertyChanged("EventListFiltered");
            }
        }

        public List<string> Filters
        {
            get => _filters;
            set
            {
                _filters = new List<string>();
                
                //TODO
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
                        //TODO
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
                    //RaisePropertyChanged();
                }
            }
        }
        public EventListVM()
        {
            Filters = new List<string>();
            //SelectedFilter = Filters.First();
            //Filter = "";
            EventRepository eventRepository = new EventRepository();
            EventList = new ObservableCollection<EventVM>(eventRepository.GetEvents());
            //OpenAddEvent = new RelayCommand(OpenAddEventPage);
            //OpenEditEvent = new RelayCommand(OpenEditEventPage);
            //DeleteEventCommand = new RelayCommand(DeleteEvent);
            //OpenSingleEvent = new RelayCommand(OpenSingleEventPage);
        }
    }
}
