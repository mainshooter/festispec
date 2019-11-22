using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.ComponentModel;

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

        public string BirthdayDate => Birthday.ToString("d");

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
                if (Firstname.Length > 45)
                {
                    return "Voornaam mag niet langer zijn dan 45 karakters";
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
                    error = ValidateFirstname;
                    break;
                case "Prefix":
                    error = ValidateFirstname;
                    break;
                case "Street":
                    error = ValidateFirstname;
                    break;
                case "City":
                    error = ValidateFirstname;
                    break;
                case "HouseNumber":
                    error = ValidateFirstname;
                    break;
                case "HouseNumberAddition":
                    error = ValidateFirstname;
                    break;
                case "Email":
                    error = ValidateFirstname;
                    break;
                case "PostalCode":
                    error = ValidateFirstname;
                    break;
                case "Phone":
                    error = ValidateFirstname;
                    break;
                case "Iban":
                    error = ValidateFirstname;
                    break;
                case "Password":
                    error = ValidateFirstname;
                    break;
                case "Status":
                    error = ValidateFirstname;
                    break;
                case "Department":
                    error = ValidateFirstname;
                    break;
                case "Birthday":
                    error = ValidateFirstname;
                    break;

            }
            return error;
        }
        public static readonly string[] ValidatedProperties =
        {
            "Firstname", "Lastname", "Prefix", "Street", "City", "HouseNumber", "HouseNumberAddition", "Email", "PostalCode", "Phone", "Iban", "Password", "Status", "Department", "Birthday"
        };

    }
}
