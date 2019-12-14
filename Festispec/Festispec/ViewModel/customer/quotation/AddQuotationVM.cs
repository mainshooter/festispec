using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Quotation;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.quotation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.quotation
{
    public class AddQuotationVM : ViewModelBase
    {
        public EventVM Event { get; set; }
        public QuotationVM Quotation { get; set; }
        public ICommand AddQuotationCommand { get; set; }
        public ICommand CloseAddQuotationCommand { get; set; }

        public AddQuotationVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                Event = message.Event;
                Quotation.Event = message.Event;
                Quotation.Customer = message.Event.Customer;
                RaisePropertyChanged("Quotation");
            });

            Quotation = new QuotationVM();
            Quotation.Status = "Open";
            AddQuotationCommand = new RelayCommand(AddQuotation, CanAddQuotation);
            CloseAddQuotationCommand = new RelayCommand(CloseAddQuotation);

        }

        public void AddQuotation()
        {
            Quotation.TimeSend = DateTime.Now;
            var userSession = UserSessionVm.Current;
            Quotation.Employee = userSession.Employee;

            using (var context = new Entities())
            {
                Quotation temp = Quotation.ToModel();
                temp.Customer = null;
                temp.Employee = null;
                temp.Event = null;

                context.Quotations.Add(temp);
                context.SaveChanges();
            }

            var tempList = Event.Quotations;
            tempList.Add(Quotation);
            Event.Quotations = tempList;

            CloseAddQuotation();
        }

        private void CloseAddQuotation()
        {
            Quotation = new QuotationVM();
            Quotation.Event = Event;
            Quotation.Customer = Event.Customer;
            RaisePropertyChanged("Quotation");
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(QuotationPage) });
        }

        public bool CanAddQuotation()
        {
            return Quotation.IsValid;
        }
    }
}
