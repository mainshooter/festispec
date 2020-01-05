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
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Planning;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Customer.Quotation;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using Hanssens.Net;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EventListVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filters;
        private bool _showOnlyFuture;

        public CustomerVM Customer { get; set; }
        public ICommand OpenPlanningCommand { get; set; }
        public ICommand OpenSurveyCommand { get; set; }
        public ICommand OpenReportCommand { get; set; }
        public ICommand OpenAddEventCommand { get; set; }
        public ICommand OpenEditEventCommand { get; set; }
        public ICommand OpenSingleEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ICommand OpenQuotationsCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SynchEventCommand { get; set; }
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
            OpenAddEventCommand = new RelayCommand(OpenAddEventPage);
            OpenEditEventCommand = new RelayCommand<EventVM>(OpenEditEventPage, CanOpenEdit);
            DeleteEventCommand = new RelayCommand<EventVM>(DeleteEvent, CanOpenDelete);
            OpenSingleEventCommand = new RelayCommand<EventVM>(OpenSingleEventPage);
            OpenSurveyCommand = new RelayCommand<EventVM>(OpenSurveyPage, HasOrder);
            OpenReportCommand = new RelayCommand<EventVM>(OpenReportPage, HasOrder);
            OpenPlanningCommand = new RelayCommand<EventVM>(OpenPlanningPage, HasOrder);
            OpenQuotationsCommand = new RelayCommand<EventVM>(OpenQuotationPage);
            BackCommand = new RelayCommand(Back);
            SynchEventCommand = new RelayCommand<EventVM>(SynchEvent);

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
            if (!Customer.ContactPersons.Any())
            {
                MessageBox.Show("Je kan geen evenement aanmaken, omdat deze klant geen contactpersonen heeft.");
                return;
            }

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
                EventList = this
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
            if (source.ContainsModelOrder()) return false;
            if (source.EndDate < DateTime.Today) return false;
            return true;
        }

        public void OpenSingleEventPage(EventVM source)
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SingleEventPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = source,
                EventList = this,
            });
        }

        public void OpenPlanningPage(EventVM source)
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = source,
                EventList = this,
            });
        }

        public void OpenSurveyPage(EventVM source)
        {
            if (source.OrderVM.Survey.Id == 0)
            {
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddSurveyPage) });
            }
            else
            {
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
            }

            MessengerInstance.Send<ChangeSelectedSurveyMessage>(new ChangeSelectedSurveyMessage() { NextSurvey = source.OrderVM.Survey });
        }

        public void OpenReportPage(EventVM source)
        {
            if (source.OrderVM.Report.Id == 0)
            {
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddReportPage) });
                MessengerInstance.Send<ChangeSelectedOrderMessage>(new ChangeSelectedOrderMessage() { SelectedOrderVM = source.OrderVM });
            }
            else
            {
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
            }

            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage() { NextReportVM = source.OrderVM.Report });
        }

        private bool HasOrder(EventVM source)
        {
            return source != null && source.HasOrder();
        }

        public void OpenQuotationPage(EventVM source)
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(QuotationPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = source
            });
        }

        public void RefreshEvents()
        {
            EventList = Customer.Events;
            RaisePropertyChanged("EventListFiltered");
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerPage) });
        }

        private void SynchEvent(EventVM source)
        {
            if (source == null) return;

            using (var storage = new LocalStorage())
            {
                storage.Store(source.Id.ToString(), source);
                storage.Persist();
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Evenement gesynchroniseerd.");
            }
        }
    }
}
