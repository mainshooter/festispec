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
using System.Windows;
using Festispec.Domain;
using System.Linq;
using System.Data.Entity;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.planning;
using Festispec.Lib.Enums;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.report;

namespace Festispec.ViewModel.customer.quotation
{
    public class QuotationListVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filters;
        private EventVM _event;
        private ObservableCollection<QuotationVM> _quotationList;

        public ICommand OpenAddQuotationCommand { get; set; }
        public ICommand OpenEditQuotationCommand { get; set; }
        public ICommand DeleteQuotationCommand { get; set; }
        public ICommand OpenSingleQuotationCommand { get; set; }
        public ICommand AcceptQuotationCommand { get; set; }
        public ICommand DeclineQuotationCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public string SelectedFilter { get; set; }

        public ObservableCollection<QuotationVM> QuotationList 
        { 
            get
            {
                return _quotationList;
            }
            set
            {
                _quotationList = value;
                RaisePropertyChanged("QuotationListFiltered");
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
                RaisePropertyChanged("Title");
            } 
        }

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
                RaisePropertyChanged("QuotationListFiltered");
            }
        }

        public List<string> Filters
        {
            get => _filters;
            set
            {
                _filters = new List<string>();
                _filters.Add("Prijs");
                _filters.Add("BTW percentage");
                _filters.Add("Verzend tijd");
                _filters.Add("Status");
            }
        }

        public ObservableCollection<QuotationVM> QuotationListFiltered
        {
            get
            {

                if (QuotationList == null)
                {
                    return QuotationList;
                }

                if (Filter != null || !Filter.Equals(""))
                {
                    switch (SelectedFilter)
                    {
                        case "Prijs":
                            return new ObservableCollection<QuotationVM>(QuotationList.Select(quotation => quotation).Where(quotation => quotation.Price.ToString().Contains(Filter.ToLower())).ToList());
                        case "BTW percentage":
                            return new ObservableCollection<QuotationVM>(QuotationList.Select(quotation => quotation).Where(quotation => quotation.VatPercentage.ToString().Contains(Filter.ToLower())).ToList());
                        case "Verzend tijd":
                            return new ObservableCollection<QuotationVM>(QuotationList.Select(quotation => quotation).Where(quotation => quotation.TimeSend.ToString().Contains(Filter.ToLower())).ToList());
                        case "Status":
                            return new ObservableCollection<QuotationVM>(QuotationList.Select(quotation => quotation).Where(quotation => quotation.Status.ToLower().Contains(Filter.ToLower())).ToList());
                    }
                }

                return QuotationList;
            }
        }

        public QuotationListVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                Event = message.Event;
                QuotationList = message.Event.Quotations;
                RaisePropertyChanged("QuotationListFiltered");
            });

            Filters = new List<string>();
            SelectedFilter = "";
            Filter = "";

            OpenAddQuotationCommand = new RelayCommand(OpenAddQuotationPage, CanAdd);
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

        private bool CanAdd()
        {
            if (QuotationList == null)
            {
                return false;
            }
            foreach (QuotationVM quot in QuotationList)
            {
                if (quot.Status.Equals(QuotationStatus.Geaccepteerd.ToString()))
                {
                    return false;
                }
            }
            return true;
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
            source.Status = QuotationStatus.Geaccepteerd.ToString();

            OrderVM order = new OrderVM();
            var userSession = UserSessionVM.Current;

            order.Event = Event;
            order.Employee = userSession.Employee;
            order.Quotation = source;
            order.Customer = Event.Customer;
            order.Survey = new SurveyVM(order);
            order.Report = new ReportVM(order);

            Event.OrderVM = order;

            ObservableCollection<DayVM> days = new ObservableCollection<DayVM>();
            int counter = Event.EndDate.Subtract(Event.BeginDate).Days;

            for (int x = 0; x <= counter; x++)
            {
                DayVM temp = new DayVM();
                temp.BeginTime = Event.BeginDate.AddDays(x);
                temp.EndTime = Event.BeginDate.AddDays(x).AddHours(23).AddMinutes(59);
                days.Add(temp);
            }

            order.Days = days;

            using (var context = new Entities())
            {
                context.Entry(source.ToModel()).State = EntityState.Modified;
                context.Orders.Add(order.ToModel());

                foreach (DayVM day in days)
                {
                    day.Order = order;
                    context.Days.Add(day.ToModel());
                }

                context.SaveChanges();
            }
            RaisePropertyChanged("QuotationListFiltered");
        }

        private void DeclineQuotation(QuotationVM source)
        {
            source.Status = QuotationStatus.Geweigerd.ToString();

            using (var context = new Entities())
            {
                context.Entry(source.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            RaisePropertyChanged("QuotationListFiltered");
        }

        private bool CanUseCommand(QuotationVM source)
        {
            if (source == null)
            {
                return false;
            }
            if (source.Status.Equals(QuotationStatus.Geweigerd.ToString()))
            {
                return false;
            }

            return CanAdd();
        }

        public void RefreshQuotations()
        {
            QuotationList = Event.Quotations;
            RaisePropertyChanged("QuotationListFiltered");
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }
    }
}
