using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Customer.Quotation;
using Festispec.ViewModel.employee.quotation;
using Festispec.ViewModel.customer.customerEvent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Windows;
using Festispec.Domain;
using System.Linq;
using System.Data.Entity;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.auth;

namespace Festispec.ViewModel.customer.quotation
{
    public class QuotationListVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filters;

        public EventVM Event { get; set; }
        public ObservableCollection<QuotationVM> QuotationList { get; set; }
        public ICommand OpenAddQuotationCommand { get; set; }
        public ICommand OpenEditQuotationCommand { get; set; }
        public ICommand DeleteQuotationCommand { get; set; }
        public ICommand OpenSingleQuotationCommand { get; set; }
        public ICommand AcceptQuotationCommand { get; set; }
        public ICommand DeclineQuotationCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public string SelectedFilter { get; set; }

        public string Title
        {
            get
            {
                if (Event != null)
                {
                    return "Offertes van " + Event.Name;
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
            //    RaisePropertyChanged("QuotationListFiltered");
            }
        }

        public List<string> Filters
        {
            get => _filters;
            set
            {
                _filters = new List<string>();
            //    _filters.Add("Naam");
            //    _filters.Add("Begindatum");
            //    _filters.Add("Bezoekersaantal");
            //    _filters.Add("Contactpersoon");
            }
        }

        public ObservableCollection<QuotationVM> QuotationListFiltered
        {
            get
            {
                //    var temp = new ObservableCollection<EventVM>();

                //    if (EventList == null)
                //    {
                //        return temp;
                //    }

                //    if (Filter != null || !Filter.Equals(""))
                //    {
                //        switch (SelectedFilter)
                //        {
                //            case "Naam":
                //                temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.Name.ToLower().Contains(Filter.ToLower())).ToList());
                //                break;
                //            case "Begindatum":
                //                temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.BeginDate.ToString().Contains(Filter.ToLower())).ToList());
                //                break;
                //            case "Bezoekersaantal":
                //                temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.AmountVisitors.ToString().Contains(Filter.ToLower())).ToList());
                //                break;
                //            case "Contactpersoon":
                //                temp = new ObservableCollection<EventVM>(EventList.Select(eventcon => eventcon).Where(eventcon => eventcon.ContactPerson.Fullname.ToLower().Contains(Filter.ToLower())).ToList());
                //                break;
                //        }

                //        if (_showOnlyFuture)
                //        {
                //            temp = new ObservableCollection<EventVM>(temp.ToList().Where(i => i.EndDate >= DateTime.Today).ToList());
                //        }
                //    }

                //    return temp;
                return QuotationList;
            }
        }

        public QuotationListVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                Event = message.Event;
                QuotationList = message.Event.Quotations;
                RaisePropertyChanged("QuotationList");
                RaisePropertyChanged("Title");
            });

            Filters = new List<string>();
            SelectedFilter = "";
            Filter = "";

            OpenAddQuotationCommand = new RelayCommand(OpenAddQuotationPage);
            OpenEditQuotationCommand = new RelayCommand<QuotationVM>(OpenEditQuotationPage, CanUseCommand);
            DeleteQuotationCommand = new RelayCommand<QuotationVM>(DeleteQuotation, CanUseCommand);
            OpenSingleQuotationCommand = new RelayCommand<QuotationVM>(OpenSingleQuotationPage);
            AcceptQuotationCommand = new RelayCommand<QuotationVM>(AcceptQuotation, CanUseCommand);
            DeclineQuotationCommand = new RelayCommand<QuotationVM>(DeclineQuotation, CanUseCommand);
            BackCommand = new RelayCommand(Back);

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(QuotationPage) && QuotationList != null)
                {
                    this.RefreshQuotations();
                }
            });
        }  

        private void OpenAddQuotationPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddQuotationPage) });
            MessengerInstance.Send<ChangeSelectedEventMessage>(new ChangeSelectedEventMessage()
            {
                Event = Event
            }) ;
        }

        private void OpenEditQuotationPage(QuotationVM source)
        {

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditQuotationPage) });
            MessengerInstance.Send<ChangeSelectedQuotationMessage>(new ChangeSelectedQuotationMessage()
            {
               Quotation = source
            });
        }

        private void OpenSingleQuotationPage(QuotationVM source)
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SingleQuotationPage) });
            MessengerInstance.Send<ChangeSelectedQuotationMessage>(new ChangeSelectedQuotationMessage()
            {
                Quotation = source
            });
        }

        private void DeleteQuotation(QuotationVM source)
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze offerte wilt verwijderen?", "Offerte Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    var temp = source.ToModel();
                    context.Quotations.Remove(context.Quotations.Where(quotation => quotation.Id == temp.Id).First());
                    context.SaveChanges();
                }
                Event.Quotations.Remove(source);
                RefreshQuotations();
            }
        }

        private void AcceptQuotation(QuotationVM source)
        {
            source.Status = "Geaccepteerd";

            OrderVM order = new OrderVM();
            var userSession = UserSessionVm.Current;

            order.Event = Event;
            order.Employee = userSession.Employee;
            order.Quotation = source;
            order.Customer = Event.Customer;

            Event.OrderVM = order;

            using (var context = new Entities())
            {
                context.Entry(source.ToModel()).State = EntityState.Modified;
                context.Orders.Add(order.ToModel());
                context.SaveChanges();
            }
        }

        private void DeclineQuotation(QuotationVM source)
        {
            source.Status = "Geweigerd";

            using (var context = new Entities())
            {
                context.Entry(source.ToModel()).State = EntityState.Modified;
            }
        }

        private bool CanUseCommand(QuotationVM source)
        {
            if (source == null || source.Status.Equals("Geweigerd") || source.Status.Equals("Geaccepteerd"))
            {
                return false;
            }
            return true;
        }

        public void RefreshQuotations()
        {
            QuotationList = Event.Quotations;
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }
    }
}
