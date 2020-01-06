using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Festispec.Domain;
using Festispec.ViewModel.customer.contactPerson.note;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class ContactPersonVM : IDataErrorInfo
    {
        private ContactPerson _contactPerson;

        public int Id
        {
            get
            {
                return _contactPerson.Id;
            }
            private set
            {
                _contactPerson.Id = value;
            }
        }

        public string Firstname
        {
            get
            {
                return _contactPerson.Firstname;
            }
            set
            {
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

        public string Lastname
        {
            get
            {
                return _contactPerson.Lastname;
            }
            set
            {
                _contactPerson.Lastname = value;
            }
        }

        public string Phone
        {
            get
            {
                return _contactPerson.Phone;
            }
            set
            {
                _contactPerson.Phone = value;
            }
        }

        public string Email
        {
            get
            {
                return _contactPerson.Email;
            }
            set
            {
                _contactPerson.Email = value;
            }
        }

        public string Function
        {
            get
            {
                return _contactPerson.Function;
            }
            set
            {
                _contactPerson.Function = value;
            }
        }

        public int CustomerId
        {
            get
            {
                return _contactPerson.CustomerId;
            }
            set
            {
                _contactPerson.CustomerId = value;
            }
        }

        public ObservableCollection<NoteVM> Notes { get; set; }

        public ContactPersonVM(ContactPerson contactPerson)
        {
            _contactPerson = contactPerson;
            Notes = new ObservableCollection<NoteVM>(_contactPerson.Notes.Select(n => new NoteVM(n)).OrderBy(n => n.Time));
        }

        public ContactPersonVM()
        {
            _contactPerson = new ContactPerson();
        }

        public ContactPerson ToModel()
        {
            return _contactPerson;
        }

        #region Validation

        string IDataErrorInfo.Error => null;

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        private string ValidateFirstname
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Firstname))
                {
                    return "Voornaam moet ingevuld zijn";
                }
                else if (Firstname.Length > 45)
                {
                    return "Voornaam mag niet langer zijn dan 45 karakters";
                }
                return null;
            }
        }

        private string ValidateLastname
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Lastname))
                {
                    return "Achternaam moet ingevuld zijn";
                }
                else if (Lastname.Length > 45)
                {
                    return "Achternaam mag niet langer zijn dan 45 karakters";
                }
                return null;
            }
        }

        private string ValidatePrefix
        {
            get
            {
                if (Prefix != null)
                {
                    if (Prefix.Length > 45)
                    {
                        return "Tussenvoegsel mag niet langer zijn dan 45 karakters";
                    }
                }
                return null;
            }
        }

        private string ValidateEmail
        {
            get
            {
                string regexEmail = "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                if (String.IsNullOrWhiteSpace(Email))
                {
                    return "Email mag niet leeg zijn";
                }
                else if (Email.Length > 100)
                {
                    return "Email mag niet meer dan 100 karakters hebben";
                }
                else if (!Regex.IsMatch(Email, regexEmail))
                {
                    return "Email voldoet niet aan het email format";
                }
                return null;
            }
        }

        private string ValidatePhone
        {
            get
            {
                string regexPhone = "^\\s*(?:\\+?(\\d{1,3}))?([-. (]*(\\d{3})[-. )]*)?((\\d{3})[-. ]*(\\d{2,4})(?:[-.x ]*(\\d+))?)\\s*$";
                if (String.IsNullOrWhiteSpace(Phone))
                {
                    return "Telefoonnummer moet ingevuld zijn";
                }
                else if (Phone.Length > 15)
                {
                    return "Telefoonnummer mag niet langer zijn dan 15 karakters";
                }
                else if (!Regex.IsMatch(Phone, regexPhone))
                {
                    return "Telefoonnummer voldoet niet aan een telefoonnummer format";
                }
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var item in ValidatedProperties)
                {
                    if (GetValidationError(item) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        string GetValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "Firstname":
                    error = ValidateFirstname;
                    break;
                case "Lastname":
                    error = ValidateLastname;
                    break;
                case "Prefix":
                    error = ValidatePrefix;
                    break;
                case "Email":
                    error = ValidateEmail;
                    break;
                case "Phone":
                    error = ValidatePhone;
                    break;
            }
            return error;
        }

        public static readonly string[] ValidatedProperties =
        {
            "Firstname", "Lastname", "Prefix",  "Email", "Phone"
        };

        #endregion
    }
}
