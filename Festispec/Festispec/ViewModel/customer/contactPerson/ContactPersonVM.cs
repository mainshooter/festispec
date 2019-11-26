using Festispec.Domain;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class ContactPersonVM
    {
        private ContactPerson _contactPerson;

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

        public string Prefix
        {
            get
            {
                return _contactPerson.Prefix;
            }
            set
            {
                _contactPerson.Prefix = value;
            }
        }

        public string Fullname
        {
            get
            {
                if (Prefix != null)
                {
                    return Firstname + " " + Prefix + " " + Lastname;
                }
                return Firstname + " " + Lastname;
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

        public int CustomerID
        {
            get
            {
                return _contactPerson.CustomerId;
            }
        }

        public ContactPersonVM(ContactPerson contactPerson)
        {
            _contactPerson = contactPerson;
        }

        public ContactPersonVM()
        {
            _contactPerson = new ContactPerson();
        }

        public ContactPerson ToModel()
        {
            return _contactPerson;
        }
    }
}
