using Festispec.Domain;
using Festispec.ViewModel.customer;
using System;

namespace Festispec.ViewModel.employee.quotation
{
    public class QuotationVM
    {
        private Quotation _quotation;

        public int Id => _quotation.Id;
        public CustomerVM Customer { get; set; }
        public EmployeeVM Employee { get; set; }

        public decimal Price
        {
            get => _quotation.Price;
            set => _quotation.Price = value;
        }

        public int VatPercentage
        {
            get => _quotation.BtwPercentage;
            set => _quotation.BtwPercentage = value;
        }

        public DateTime TimeSend
        {
            get => (DateTime) _quotation.TimeSend;
            set => _quotation.TimeSend = value;
        }

        public string content
        {
            get => _quotation.Content;
            set => _quotation.Content = value;
        }

        public QuotationVM(Quotation quotation)
        {
            _quotation = quotation;
            Customer = new CustomerVM(_quotation.Customer);
            Employee = new EmployeeVM(_quotation.Employee);
        }

        public QuotationVM()
        {
            _quotation = new Quotation();
        }
    }
}
