using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace Festispec.ViewModel.employee
{
    public class EmployeeVM : ViewModelBase
    {
        private DepartmentVM _department;
        private Employee _employee;

        public int Id
        {
            get
            {
                return _employee.Id;
            }
        }

        public DepartmentVM Department
        {
            get
            {
                return _department;
            }
            set
            {
                if (value != null)
                {
                    _department = value;
                    _employee.Department = value.Name;
                    _employee.Department1 = value.ToModel();
                }
            }
        }

        public Department DepartmentModel
        {
            get
            {
                return _employee.Department1;
            }
            set
            {
                _employee.Department1 = value;
            }
        }

        public string Firstname
        {
            get
            {
                return _employee.Firstname;
            }
            set
            {
                _employee.Firstname = value;
                RaisePropertyChanged();
            }
        }

        public string Prefix
        {
            get
            {
                return _employee.Prefix;
            }
            set
            {
                _employee.Prefix = value;
            }
        }

        public string Lastname
        {
            get
            {
                return _employee.Lastname;
            }
            set
            {
                _employee.Lastname = value;
            }
        }

        public string Street
        {
            get
            {
                return _employee.Street;
            }
            set
            {
                _employee.Street = value;
            }
        }

        public int HouseNumber
        {
            get
            {
                return _employee.HouseNumber;
            }
            set
            {
                _employee.HouseNumber = value;
            }
        }

        public string HouseNumberAddition
        {
            get
            {
                return _employee.HouseNumber_Addition;
            }
            set
            {
                _employee.HouseNumber_Addition = value;
            }
        }

        public string FullHouseNumber
        {
            get
            {
                return (HouseNumber + HouseNumberAddition);
            }
        }

        public string PostalCode
        {
            get
            {
                return _employee.PostalCode;
            }
            set
            {
                _employee.PostalCode = value;
            }
        }

        public string City
        {
            get
            {
                return _employee.City;
            }
            set
            {
                _employee.City = value;
            }
        }

        public string Email
        {
            get
            {
                return _employee.Email;
            }
            set
            {
                _employee.Email = value;
            }
        }

        public string Phone
        {
            get
            {
                return _employee.Phone;
            }
            set
            {
                _employee.Phone = value;
            }
        }

        public string Password
        {
            get
            {
                return _employee.Password;
            }
            set
            {
                _employee.Password = value;
            }
        }

        private string PasswordResetToken
        {
            get
            {
                return _employee.PasswordResetToken;
            }
            set
            {
                _employee.PasswordResetToken = value;
            }
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
            set
            {
                _employee.ResetTokenEndTime = value;
            }
        }

        public string Iban
        {
            get
            {
                return _employee.Iban;
            }
            set
            {
                _employee.Iban = value;
            }
        }

        public string Status
        {
            get
            {
                return _employee.Status;
            }
            set
            {
                _employee.Status = value;
            }
        }

        public DateTime Birthday
        {
            get
            {
                return _employee.Birthday.Date;
            }
            set
            {
                _employee.Birthday = value.Date;
            }
        }

        public string BirthdayDate
        {
            get
            {
                return Birthday.ToString("d");
            }
        }

        [PreferredConstructor]
        public EmployeeVM()
        {
            _employee = new Employee();
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
    }
}
