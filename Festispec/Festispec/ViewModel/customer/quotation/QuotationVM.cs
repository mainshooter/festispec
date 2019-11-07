using Festispec.Domain;
using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.quotation
{
    public class QuotationVM
    {
        public int Id {
            get {
                return _quotation.Id;
            }
            private set {
                _quotation.Id = value;
            }
        }
        private CustomerVM _customer;
        public CustomerVM Customer { 
            get {
                return _customer;
            }
            set {
                _customer = value;
            }
        }
        private EmployeeVM _employee;
        public EmployeeVM Employee { 
            get {
                return _employee;
            }
            set {
                _employee = value;
            }
        }
        private EventVM _eventVM;
        public EventVM Event { 
            get {
                return _eventVM;
            }
            set {
                _eventVM = value;
            }
        }
        public decimal Price {
            get {
                return _quotation.Price;
            }
            set {
                _quotation.Price = value;
            }
        }
        public int VatPercentage {
            get {
                return _quotation.BtwPercentage;
            }
            set {
                _quotation.BtwPercentage = value;
            }
        }
        public DateTime TimeSend {
            get {
                return (DateTime) _quotation.TimeSend;
            }
            set {
                _quotation.TimeSend = value;
            }
        }
        public string content {
            get {
                return _quotation.Content;
            }
            set {
                _quotation.Content = value;
            }
        }

        private Quotation _quotation;
        public QuotationVM(Quotation quotation)
        {
            _quotation = quotation;
            Customer = new CustomerVM(_quotation.Customer);
            Employee = new EmployeeVM(_quotation.Employee);
            Event = new EventVM(_quotation.Event);
        }

        public QuotationVM()
        {
            _quotation = new Quotation();
        }
    }
}
