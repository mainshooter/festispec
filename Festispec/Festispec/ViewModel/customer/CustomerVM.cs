using Festispec.ViewModel.customer.contactPerson;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.quotation;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace Festispec.ViewModel.customer
{
    public class CustomerVM
    {
        private Domain.Customer _customer;

        public int Id 
        {
            get => _customer.Id;
            private set => _customer.Id = value;
        }

        public string Name 
        {
            get => _customer.Name;
            set => _customer.Name = value;
        }

        public int COC 
        {
            get => _customer.COC;
            set => _customer.COC = value;
        }

        public int EstablishmentNumber => _customer.BranchNumber;

        public string Street 
        {
            get => _customer.Street;
            set => _customer.Street = value;
        }

        public int HouseNumber 
        {
            get => _customer.HouseNumber;
            set => _customer.HouseNumber = value;
        }

        public string PostalCode 
        {
            get => _customer.PostalCode;
            set => _customer.PostalCode = value;
        }

        public string Phone 
        {
            get => _customer.Phone;
            set => _customer.Phone = value;
        }

        public string Email 
        {
            get => _customer.Email;
            set => _customer.Email = value;
        }

        public string Website 
        {
            get => _customer.Website;
            set => _customer.Website = value;
        }

        [JsonIgnore]
        public ObservableCollection<EventVM> Events { get; set; }

        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

        public ObservableCollection<QuotationVM> Quotations { get; set; }

        public CustomerVM(Domain.Customer customer)
        {
            _customer = customer;
            Events = new ObservableCollection<EventVM>(customer.Events.Select(eventcon => new EventVM(eventcon, this)));
            ContactPersons = new ObservableCollection<ContactPersonVM>(customer.ContactPersons.Select(contactPerson => new ContactPersonVM(contactPerson)));
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
