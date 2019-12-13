using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Festispec.ViewModel.employee
{
    public class EmployeeVM : ViewModelBase, IDataErrorInfo
    {
        private DepartmentVM _department;
        private Employee _employee;

        public int Id => _employee.Id;

        public DepartmentVM Department
        {
            get => _department;
            set
            {
                if (value == null) return;
                _department = value;
                _employee.Department = value.Name;
                _employee.Department1 = value.ToModel();
            }
        }

        [JsonIgnore]
        public Department DepartmentModel
        {
            get => _employee.Department1;
            set => _employee.Department1 = value;
        }

        public string Firstname
        {
            get => _employee.Firstname;
            set
            {
                _employee.Firstname = value;
            }
        }

        public string Prefix
        {
            get => _employee.Prefix;
            set => _employee.Prefix = value;
        }

        public string Lastname
        {
            get => _employee.Lastname;
            set => _employee.Lastname = value;
        }

        public string Fullname => Firstname + " " + Prefix + " " + Lastname;

        public string Street
        {
            get => _employee.Street;
            set => _employee.Street = value;
        }

        public int HouseNumber
        {
            get => _employee.HouseNumber;
            set => _employee.HouseNumber = value;
        }

        public string HouseNumberAddition
        {
            get => _employee.HouseNumber_Addition;
            set => _employee.HouseNumber_Addition = value;
        }

        public string FullHouseNumber => (HouseNumber + HouseNumberAddition);

        public string PostalCode
        {
            get => _employee.PostalCode;
            set => _employee.PostalCode = value;
        }

        public string City
        {
            get => _employee.City;
            set => _employee.City = value;
        }

        public string Email
        {
            get => _employee.Email;
            set => _employee.Email = value;
        }

        public string Phone
        {
            get => _employee.Phone;
            set => _employee.Phone = value;
        }

        public string Password
        {
            get => _employee.Password;
            set => _employee.Password = value;
        }

        private string PasswordResetToken
        {
            get => _employee.PasswordResetToken;
            set => _employee.PasswordResetToken = value;
        }

        public DateTime ResetTokenEndTime
        {
            get
            {
                if (_employee.ResetTokenEndTime != null)
                {
                    return (DateTime)_employee.ResetTokenEndTime;
                }
                return DateTime.MinValue;
            }
            set => _employee.ResetTokenEndTime = value;
        }

        public string Iban
        {
            get => _employee.Iban;
            set => _employee.Iban = value;
        }

        public string Status
        {
            get => _employee.Status;
            set => _employee.Status = value;
        }

        public DateTime Birthday
        {
            get => _employee.Birthday.Date;
            set => _employee.Birthday = value.Date;
        }

        public string BirthdayDate => Birthday.ToString("dd-MM-yyyy");

        [PreferredConstructor]
        public EmployeeVM()
        {
            _employee = new Employee();
            Birthday = DateTime.Parse("01/01/2000");
        }

        public EmployeeVM(Employee employee)
        {
            _employee = employee;
            Department = new DepartmentVM(_employee.Department1);
        }

        public Employee ToModel()
        {
            return _employee;
        }

        public bool IsInDepartment(string department)
        {
            throw new NotImplementedException();
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

        private string ValidateBirthday
        {
            get
            {
                if (Birthday == null)
                {
                    return "Geboortedatum mag niet leeg zijn";
                }
                else if (Birthday.Year < 1800)
                {
                    return "Geboortedatum moet minimaal in het jaar 1800 zijn";
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

        private string ValidatePassword
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Password))
                {
                    return "Wachtwoord moet ingevuld zijn";
                }
                else if (Password.Length > 255)
                {
                    return "Wachtwoord mag niet langer zijn dan 255 karakters";
                }
                return null;
            }
        }

        private string ValidateIban
        {
            get
            {
                string regexIban = "^[A-Z]{2}[ -]?[0-9]{2}[ -]?[A-Z]{4}[ -]?[0-9]{4}[ -]?[0-9]{4}[ -]?[0-9]{2}$";
                if (String.IsNullOrWhiteSpace(Iban))
                {
                    return "Iban moet ingevuld zijn";
                }
                else if (Iban.Length > 27)
                {
                    return "Iban mag niet langer zijn dan 27 karakters";
                }
                else if (!Regex.IsMatch(Iban, regexIban))
                {
                    return "Iban voldoet niet aan een iban formaat";
                }
                return null;
            }
        }

        private string ValidateStatus
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Status))
                {
                    return "Status moet ingevuld zijn";
                }
                return null;
            }
        }

        private string ValidateDepartment
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Department.Name))
                {
                    return "Afdeling moet ingevuld zijn";
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
                case "Firstname":
                    error = ValidateFirstname;
                    break;
                case "Lastname":
                    error = ValidateLastname;
                    break;
                case "Prefix":
                    error = ValidatePrefix;
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
                case "Email":
                    error = ValidateEmail;
                    break;
                case "PostalCode":
                    error = ValidatePostalCode;
                    break;
                case "Phone":
                    error = ValidatePhone;
                    break;
                case "Iban":
                    error = ValidateIban;
                    break;
                case "Password":
                    error = ValidatePassword;
                    break;
                case "Status":
                    error = ValidateStatus;
                    break;
                case "Department":
                    error = ValidateDepartment;
                    break;
                case "Birthday":
                    error = ValidateBirthday;
                    break;

            }
            return error;
        }

        public static readonly string[] ValidatedProperties =
        {
            "Firstname", "Lastname", "Prefix", "Street", "City", "HouseNumber", "HouseNumberAddition", "Email", "PostalCode", "Phone", "Iban", "Password", "Status", "Department", "Birthday"
        };

        #endregion
    }
}
