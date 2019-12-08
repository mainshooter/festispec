using Festispec.Domain;
using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;
using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;

namespace Festispec.ViewModel.employee.quotation
{
    public class QuotationVM : ViewModelBase, IDataErrorInfo
    {
        private Quotation _quotation;
        private CustomerVM _customer;
        private EmployeeVM _employee;
        private EventVM _event;

        public int Id => _quotation.Id;

        public CustomerVM Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                _quotation.CustomerId = value.Id;
            }
        }
        public EmployeeVM Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                _quotation.EmployeeId = value.Id;
            }
        }
        public EventVM Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                _quotation.EventId = value.Id;
            }
        }

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
            get => (DateTime)_quotation.TimeSend;
            set => _quotation.TimeSend = value;
        }

        public string Content
        {
            get => _quotation.Content;
            set => _quotation.Content = value;
        }

        public string Status
        {
            get => _quotation.Status;
            set => _quotation.Status = value;
        }

        public QuotationVM(Quotation quotation, CustomerVM customer)
        {
            _quotation = quotation;
            Customer = customer;
            Employee = new EmployeeVM(_quotation.Employee);
        }

        public QuotationVM()
        {
            _quotation = new Quotation();
        }

        public Quotation ToModel()
        {
            return _quotation;
        }

        string IDataErrorInfo.Error => null;

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        #region Validation

        private string ValidatePrice
        {
            get
            {
                if (Price == 0)
                {
                    return "De Prijs kan geen 0 zijn";
                }
                else if (Price > 999999.99M)
                {
                    return "Prijs mag niet groter zijn dan 999999.99";
                }
                return null;
            }
        }

        private string ValidateVatPercentage
        {
            get
            {
                if (VatPercentage < 0)
                {
                    return "BTW percentage mag niet kleiner dan 0 zijn";
                }
                else if (VatPercentage > 100)
                {
                    return "BTW percentage mag niet groter zijn dan 100";
                }
                return null;
            }
        }

        private string ValidateContent
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Content))
                {
                    return "Een beschrijving moet toegevoegd zijn";
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
                case "Price":
                    error = ValidatePrice;
                    break;
                case "VatPercentage":
                    error = ValidateVatPercentage;
                    break;
                case "Content":
                    error = ValidateContent;
                    break;
            }
            return error;
        }
        public static readonly string[] ValidatedProperties =
        {
            "Price", "VatPercentage", "Content"
        };

        #endregion
    }
}
