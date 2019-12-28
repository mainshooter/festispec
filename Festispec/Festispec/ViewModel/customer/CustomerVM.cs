using System;
using Festispec.ViewModel.customer.contactPerson;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.quotation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace Festispec.ViewModel.customer
{
    public class CustomerVM : ViewModelBase, IDataErrorInfo
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

        public int BranchNumber
        {
            get => _customer.BranchNumber;
            set => _customer.BranchNumber = value;
        }

        public string Logo
        {
            get => _customer.Logo;
            set => _customer.Logo = value;
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

        public string City
        {
            get => _customer.City;
            set => _customer.City = value;
        }

        public string HouseNumberAddition
        {
            get => _customer.HouseNumber_Addition;
            set => _customer.HouseNumber_Addition = value;
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
        public bool IsValid
        {
            get
            {
                foreach (var property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
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

        #region Validation
        string IDataErrorInfo.Error => null;
        string IDataErrorInfo.this[string propertyName] => GetValidationError(propertyName);

        string GetValidationError(string propertyName)
        {
            string result = null;
            switch (propertyName)
            {
                case "Name":
                    result = ValidateFirstname;
                    break;
                case "COC":
                    result = ValidateNumber;
                    break;
                case "Street":
                    result = ValidateStreet;
                    break;
                case "HouseNumber":
                    result = ValidateHouseNumber;
                    break;
                case "HouseNumberAddition":
                    result = ValidateHouseNumberAddition;
                    break;
                case "City":
                    result = ValidateCity;
                    break;
                case "PostalCode":
                    result = ValidatePostalCode;
                    break;
                case "Phone":
                    result = ValidatePhone;
                    break;
                case "Email":
                    result = ValidateEmail;
                    break;
                case "Website":
                    result = ValidateWebsite;
                    break;
                case "Logo":
                    result = ValidateWebsite;
                    break;
                case "BranchNumber":
                    result = ValidateNumber;
                    break;
            }

            return result;
        }

        public static readonly string[] ValidatedProperties =
        {
            "Name", "COC", "Street", "Street", "HouseNumber", "HouseNumberAddition", "HouseNumberAddition", "City", 
            "PostalCode", "Phone", "Email", "Website", "Logo", "BranchNumber"
        };


        private string ValidateWebsite => !Uri.IsWellFormedUriString(Website, UriKind.Absolute) ? "Link ongeldig" : null;

        private string ValidateNumber
        {
            get
            {
                if (COC == 0)
                {
                    return "Nummer mag niet nul zijn";
                }
                else if (COC > 999999999)
                {
                    return "Nummer is te lang";
                }
                return null;
            }
        }

        private string ValidateEmail
        {
            get
            {
                const string regexEmail = "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                if (string.IsNullOrWhiteSpace(Email))
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
                const string regexPhone = "^\\s*(?:\\+?(\\d{1,3}))?([-. (]*(\\d{3})[-. )]*)?((\\d{3})[-. ]*(\\d{2,4})(?:[-.x ]*(\\d+))?)\\s*$";
                if (string.IsNullOrWhiteSpace(Phone))
                {
                    return "Telefoonnummer moet ingevuld zijn";
                }
                else if (Phone.Length > 100)
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

        private string ValidateFirstname
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                {
                    return "Voornaam moet ingevuld zijn";
                }
                else if (Name.Length > 45)
                {
                    return "Voornaam mag niet langer zijn dan 45 karakters";
                }
                return null;
            }
        }

        private string ValidateCity
        {
            get
            {
                if (String.IsNullOrWhiteSpace(City))
                {
                    return "Woonplaats moet ingevuld zijn";
                }
                else if (City.Length > 45)
                {
                    return "Woonplaats mag niet langer zijn dan 45 karakters";
                }
                return null;
            }
        }

        private string ValidateStreet
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Street))
                {
                    return "Straat moet ingevuld zijn";
                }
                else if (Street.Length > 100)
                {
                    return "Straat mag niet langer zijn dan 100 karakters";
                }
                return null;
            }
        }

        private string ValidateHouseNumber
        {
            get
            {
                if (HouseNumber == 0)
                {
                    return "Huisnummer mag niet nul zijn";
                }
                else if (HouseNumber > 9999)
                {
                    return "Huisnummer mag niet groter zijn dan 9999";
                }
                return null;
            }
        }

        private string ValidateHouseNumberAddition
        {
            get
            {
                if (HouseNumberAddition != null)
                {
                    if (HouseNumberAddition.Length > 5)
                    {
                        return "Huisnummer toevoeging mag niet langer zijn dan 5 karakters";
                    }
                }
                return null;
            }
        }

        private string ValidatePostalCode
        {
            get
            {
                const string regexPostalCode = "\\b[0-9]{4} ?[a-zA-Z]{2}\\b";

                if (string.IsNullOrWhiteSpace(PostalCode))
                {
                    return "Postcode mag niet leeg zijn";
                }
                else if (!Regex.IsMatch(PostalCode, regexPostalCode))
                {
                    return "Postcode voldoet niet aan de postcode format";
                }
                else if (PostalCode.Length > 6)
                {
                    return "Postcode mag niet langer zijn dan 6 karakter";
                }
                return null;
            }
        }
        #endregion Validation
    }
}
