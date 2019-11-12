using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class EmployeeVM : ViewModelBase
    {
        private DepartmentVM _department;
        private Employee _employee;
        private EmployeeListVM _employeeList;
        public ICommand CloseSingleEmployeeCommand { get; set; }

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
                _department = value;
                _employee.Department = value.Name;
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

        [PreferredConstructorAttribute]
        public EmployeeVM(Employee employee)
        {
            _employee = employee;
            Department = new DepartmentVM(_employee.Department1);
        }

        public EmployeeVM()
        {
            _employee = new Employee();
        }

        public EmployeeVM(Employee employee, EmployeeListVM employeeList)
        {
            _employeeList = employeeList;
            _employee = employee;
            Department = new DepartmentVM(_employee.Department1);
            CloseSingleEmployeeCommand = new RelayCommand(CloseSingleEmployee);
        }

        public Employee ToModel()
        {
            return _employee;
        }

        public void CloseSingleEmployee()
        {
            _employeeList.CloseTab();
        }
    }
}
