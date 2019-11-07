using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class ContactPersonVM
    {
        private CustomerVM _customer;
        public CustomerVM Customer { 
            get {
                return _customer;
            }
            private set {
                _customer = value;
            }
        }
        public int Id { 
            get {
                return _contactPerson.Id;
            }
            private set {
                _contactPerson.Id = value;
            }
        }
        public string Firstname { 
            get {
                return _contactPerson.Firstname;
            }
            set {
                _contactPerson.Firstname = value;
            }
        }
        public string Lastname {
            get {
                return _contactPerson.Lastname;
            }
            set {
                _contactPerson.Lastname = value;
            }
        }
        public string Phone { 
            get {
                return _contactPerson.Phone;
            }
            set {
                _contactPerson.Phone = value;
            }
        }
        public string Email { 
            get {
                return _contactPerson.Email;
            }
            set {
                _contactPerson.Email = value;
            }
        }
        public string Function { 
            get {
                return _contactPerson.Function;
            }
            set {
                _contactPerson.Function = value;
            }
        }

        private ContactPerson _contactPerson;
        public ContactPersonVM(ContactPerson contactPerson)
        {
            _contactPerson = contactPerson;
            _customer = new CustomerVM(_contactPerson.Customer);
        }
    }
}
