using Festispec.Domain;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.quotation;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.customer
{
    public class CustomerVM
    {
        public int Id { 
            get {
                return _customer.Id;
            }
            private set {
                _customer.Id = value;
            }
        }
        public string Name { 
            get {
                return _customer.Name;
            }
            set {
                _customer.Name = value;
            }
        }
        public int COC {
            get {
                return _customer.COC;
            }
            set {
                _customer.COC = value;
            }
        }
        public int EstablishmentNumber { 
            get {
                return _customer.BranchNumber;
            }
        }
        public string Street { 
            get {
                return _customer.Street;
            }
            set {
                _customer.Street = value;
            }
        }
        public int HouseNumber { 
            get {
                return _customer.HouseNumber;
            }
            set {
                _customer.HouseNumber = value;
            }
        }
        public string PostalCode { 
            get {
                return _customer.PostalCode;
            }
            set {
                _customer.PostalCode = value;
            }
        }
        public string Phone { 
            get {
                return _customer.Phone;
            }
            set {
                _customer.Phone = value;
            }
        }
        public string Email { 
            get {
                return _customer.Email;
            }
            set {
                _customer.Email = value;
            }
        }
        public string Website { 
            get {
                return _customer.Website;
            }
            set {
                _customer.Website = value;
            }
        }
        private ObservableCollection<EventVM> _events;
        public ObservableCollection<EventVM> Events {
            get {
                return _events;
            }
            set {
                _events = value;
            }
        }
        private ObservableCollection<QuotationVM> _quotations;
        public ObservableCollection<QuotationVM> Quotations {
            get {
                return _quotations;
            }
            set {
                _quotations = value;
            }
        }
        private Customer _customer;

        public CustomerVM(Customer customer)
        {
            _customer = customer;
            Events = new ObservableCollection<EventVM>(_customer.Events.ToList().Select(e => new EventVM(e)));
            Quotations = new ObservableCollection<QuotationVM>(_customer.Quotations.ToList().Select(q => new QuotationVM(q)));
        }

        public CustomerVM()
        {
            _customer = new Customer();
        }
    }
}
