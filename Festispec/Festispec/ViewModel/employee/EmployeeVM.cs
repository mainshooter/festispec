using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace Festispec.ViewModel.employee
{
    public class EmployeeVM : ViewModelBase
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
                RaisePropertyChanged();
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
    }
}
