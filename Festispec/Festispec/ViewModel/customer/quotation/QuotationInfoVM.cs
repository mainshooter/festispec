using Festispec.Message;
using Festispec.View.Pages.Customer.Quotation;
using Festispec.ViewModel.employee.quotation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.quotation
{
    public class QuotationInfoVM : ViewModelBase
    {
        private QuotationVM _quotation;

        public ICommand CloseSingleQuotationCommand { get; set; }

        public QuotationVM Quotation
        {
            get
            {
                return _quotation;
            }
            set
            {
                _quotation = value;
                RaisePropertyChanged("Quotation");
            }
        }

        public QuotationInfoVM()
        {
            CloseSingleQuotationCommand = new RelayCommand(CloseSingleQuotation);
            this.MessengerInstance.Register<ChangeSelectedQuotationMessage>(this, message =>
            {
                Quotation = message.Quotation;
            });
        }

        public void CloseSingleQuotation()
        {
            this.MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(QuotationPage) });
        }
    }
}
