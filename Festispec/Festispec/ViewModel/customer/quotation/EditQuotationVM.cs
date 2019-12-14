using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Quotation;
using Festispec.ViewModel.employee.quotation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Data.Entity;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.quotation
{
    public class EditQuotationVM : ViewModelBase
    {
        public QuotationVM Quotation { get; set; }
        public ICommand EditQuotationCommand { get; set; }
        public ICommand CloseEditQuotationCommand { get; set; }

        public EditQuotationVM()
        {
            this.MessengerInstance.Register<ChangeSelectedQuotationMessage>(this, message =>
            {
                Quotation = message.Quotation;
                RaisePropertyChanged("Quotation");
            });

            EditQuotationCommand = new RelayCommand(EditQuotation, CanEditQuotation);
            CloseEditQuotationCommand = new RelayCommand(CloseEditQuotation);

        }

        public void EditQuotation()
        {
            using (var context = new Entities())
            {
                Quotation temp = Quotation.ToModel();;
                temp.Customer = null;
                temp.Employee = null;
                temp.Event = null;

                context.Entry(temp).State = EntityState.Modified;
                context.SaveChanges();
            }

            CloseEditQuotation();
        }

        private void CloseEditQuotation()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(QuotationPage) });
        }

        public bool CanEditQuotation()
        {
            if (Quotation == null)
            {
                return false;
            }
            return Quotation.IsValid;
        }
    }
}
