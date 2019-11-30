using Festispec.ViewModel.customer.contactPerson;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.quotation;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.customer
{
    public class CustomerVM
    {
        private ObservableCollection<EventVM> _events;
        private ObservableCollection<ContactPersonVM> _contactPersons;
        private ObservableCollection<QuotationVM> _quotations;
        private Domain.Customer _customer;

        public int Id 
        {
            get 
            {
                return _customer.Id;
            }
            private set 
            {
                _customer.Id = value;
            }
        }

        public string Name 
        {
            get 
            {
                return _customer.Name;
            }
            set 
            {
                _customer.Name = value;
            }
        }

        public int COC 
        {
            get 
            {
                return _customer.COC;
            }
            set 
            {
                _customer.COC = value;
            }
        }

        public int EstablishmentNumber 
        {
            get 
            {
                return _customer.BranchNumber;
            }
        }

        public string Street 
        {
            get 
            {
                return _customer.Street;
            }
            set
            {
                _customer.Street = value;
            }
        }

        public int HouseNumber 
        {
            get 
            {
                return _customer.HouseNumber;
            }
            set 
            {
                _customer.HouseNumber = value;
            }
        }

        public string PostalCode 
        {
            get 
            {
                return _customer.PostalCode;
            }
            set 
            {
                _customer.PostalCode = value;
            }
        }

        public string Phone 
        {
            get 
            {
                return _customer.Phone;
            }
            set 
            {
                _customer.Phone = value;
            }
        }

        public string Email 
        {
            get 
            {
                return _customer.Email;
            }
            set 
            {
                _customer.Email = value;
            }
        }

        public string Website 
        {
            get
            {
                return _customer.Website;
            }
            set 
            {
                _customer.Website = value;
            }
        }

        public ObservableCollection<EventVM> Events 
        {
            get 
            {
                return _events;
            }
            set 
            {
                _events = value;
            }
        }

        public ObservableCollection<ContactPersonVM> ContactPersons
        {
            get
            {
                return _contactPersons;
            }
            set
            {
                _contactPersons = value;
            }
        }

        public ObservableCollection<QuotationVM> Quotations 
        {
            get 
            {
                return _quotations;
            }
            set 
            {
                _quotations = value;
            }
        }

        public CustomerVM(Domain.Customer customer)
        {
            _customer = customer;
            _events = new ObservableCollection<EventVM>(customer.Events.Select(eventcon => new EventVM(eventcon, this)));
            _contactPersons = new ObservableCollection<ContactPersonVM>(customer.ContactPersons.Select(contactPerson => new ContactPersonVM(contactPerson)));
        }

        public CustomerVM()
        {
            _customer = new Domain.Customer();
        }

        public Domain.Customer ToModel()
        {
            return _customer;
        }
    }
}
