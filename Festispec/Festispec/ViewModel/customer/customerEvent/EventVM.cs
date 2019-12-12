using Festispec.Domain;
using Festispec.ViewModel.customer.contactPerson;
using GalaSoft.MvvmLight;
using Festispec.ViewModel.Customer.order;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;
using Festispec.ViewModel.employee.quotation;
using System.Collections.ObjectModel;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EventVM : ViewModelBase , IDataErrorInfo
    {
        private Event _event;
        private CustomerVM _customer;
        private ContactPersonVM _contactPerson;
        private ObservableCollection<QuotationVM> _quotations;

        public int Id => _event.Id;

        public Domain.Customer CustomerModel
        {
            get => _event.Customer;
            set => _event.Customer = value;
        }

        public ContactPerson ContactPersonModel
        {
            get => _event.ContactPerson;
            set => _event.ContactPerson = value;
        }

        public CustomerVM Customer 
        {
            get => _customer;
            set
            {
                if (value == null) return;
                _customer = value;
                _event.CustomerId = value.Id;
                _event.Customer = value.ToModel();
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

        public ContactPersonVM ContactPerson 
        {
            get => _contactPerson;
            set
            {

                if (value == null) return;
                _contactPerson = value;
                _event.ContactPersonId = value.Id;
                _event.ContactPerson = value.ToModel();
            }
        }

        public string Name 
        {
            get => _event.Name;
            set => _event.Name = value;
        }

        public DateTime BeginDate 
        {
            get => _event.BeginDate.Date;
            set => _event.BeginDate = value.Date;
        }

        public string BeginDateDate => BeginDate.ToString("d");

        public DateTime EndDate 
        {
            get => _event.EndDate.Date;
            set => _event.EndDate = value.Date;
        }

        public string EndDateDate => EndDate.ToString("d");

        public int AmountVisitors 
        {
            get => _event.AmountVisitors;
            set => _event.AmountVisitors = value;
        }

        public int SurfaceM2 
        {
            get => _event.SurfaceM2;
            set => _event.SurfaceM2 = value;
        }

        public string Description 
        {
            get => _event.Description;
            set => _event.Description = value;
        }

        public OrderVM OrderVM { get; set; }

        public string Street
        {
            get => _event.Street;
            set => _event.Street = value;
        }

        public int HouseNumber
        {
            get => _event.HouseNumber;
            set => _event.HouseNumber = value;
        }

        public string HouseNumberAddition
        {
            get => _event.HouseNumber_Addition;
            set => _event.HouseNumber_Addition = value;
        }

        public string PostalCode
        {
            get => _event.PostalCode;
            set => _event.PostalCode = value;
        }

        public string City
        {
            get => _event.City;
            set => _event.City = value;
        }

        public EventVM(Event eventCon, CustomerVM customer)
        {
            _event = eventCon;
            Customer = customer;
            OrderVM = eventCon.Orders.Count > 0 ? new OrderVM(eventCon.Orders.FirstOrDefault(), this) : new OrderVM();
            _contactPerson = new ContactPersonVM(_event.ContactPerson);
            _quotations = new ObservableCollection<QuotationVM>(eventCon.Quotations.Select(quotation => new QuotationVM(quotation, customer, this)));
        }

        public EventVM()
        {
            _event = new Event();
            BeginDate = DateTime.Today.Date;
            EndDate = DateTime.Today.Date;
        }

        public Event ToModel()
        {
            return _event;
        }

        public bool HasOrder()
        {
            return OrderVM.Id != 0;
        }

        public bool ContainsModelOrder()
        {
            return _event.Orders.Any();
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

        private string ValidateName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                {
                    return "Naam moet ingevuld zijn";
                }
                else if (Name.Length > 45)
                {
                    return "Naam mag niet langer zijn dan 45 karakters";
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
                string regexPostalCode = "\\b[0-9]{4} ?[a-zA-Z]{2}\\b";
                if (String.IsNullOrWhiteSpace(PostalCode))
                {
                    return "Postcode mag niet nul zijn";
                }
                else if (!Regex.IsMatch(PostalCode, regexPostalCode))
                {
                    return "Postcode voldoet niet aan een postcode formaat";
                }
                else if (PostalCode.Length > 6)
                {
                    return "Postcode mag niet langer zijn dan 6 karakter";
                }
                return null;
            }
        }
        private string ValidateBeginDate
        {
            get
            {
                if (BeginDate == null)
                {
                    return "Begindatum mag niet nul zijn";
                }
                else if (BeginDate < DateTime.Today)
                {
                    return "Begindatum moet vandaag of later zijn";
                }
                return null;
            }
        }

        private string ValidateEndDate
        {
            get
            {
                if (EndDate == null)
                {
                    return "Einddatum mag niet nul zijn";
                }
                else if (EndDate < BeginDate)
                {
                    return "Einddatum moet op of na de begindatum zijn";
                }
                return null;
            }
        }
        private string ValidateAmountVisitors
        {
            get
            {
                if (AmountVisitors == 0)
                {
                    return "Bezoekersaantal mag niet nul zijn";
                }
                return null;
            }
        }
        private string ValidateSurfaceM2
        {
            get
            {
                if (SurfaceM2 == 0)
                {
                    return "Oppervlakte mag niet nul zijn";
                }
                return null;
            }
        }

        private string ValidateContactPerson
        {
            get
            {
                if (ContactPerson == null || string.IsNullOrWhiteSpace(ContactPerson.Fullname))
                {
                    return "Contactpersoon moet ingevuld zijn";
                }
                return null;
            }
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
        

        string GetValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "Name":
                    error = ValidateName;
                    break;
                case "Street":
                    error = ValidateStreet;
                    break;
                case "City":
                    error = ValidateCity;
                    break;
                case "HouseNumber":
                    error = ValidateHouseNumber;
                    break;
                case "HouseNumberAddition":
                    error = ValidateHouseNumberAddition;
                    break;
                case "PostalCode":
                    error = ValidatePostalCode;
                    break;
                case "BeginDate":
                    error = ValidateBeginDate;
                    break;
                case "EndDate":
                    error = ValidateEndDate;
                    break;
                case "AmountVisitors":
                    error = ValidateAmountVisitors;
                    break;
                case "SurfaceM2":
                    error = ValidateSurfaceM2;
                    break;
                case "ContactPerson":
                    error = ValidateContactPerson;
                    break;

            }
            return error;
        }
        public static readonly string[] ValidatedProperties =
        {
            "Name", "Street", "City", "HouseNumber", "HouseNumberAddition", "PostalCode", "BeginDate", "EndDate", "AmountVisitors", "SurfaceM2", "ContactPerson"
        };

        #endregion
    }
}
