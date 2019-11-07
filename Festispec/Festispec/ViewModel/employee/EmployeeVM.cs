using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee
{
    public class EmployeeVM
    {
        public int Id {
            get {
                return _employee.Id;
            }
            private set {
                _employee.Id = value;
            }
        }
        private DepartmentVM _department;
        public DepartmentVM Department { 
            get {
                return _department;
            }
            set {
                _department = value;
            }
        }
        public string Firstname { 
            get {
                return _employee.Firstname;
            }
            set {
                _employee.Firstname = value;
            }
        }

        public string Lastname { 
            get {
                return _employee.Lastname;
            }
            set {
                _employee.Lastname = value;
            }
        }
        public string Street { 
            get {
                return _employee.Street;
            }
            set {
                _employee.Street = value;
            }
        }
        public int HouseNumber {
            get {
                return _employee.HouseNumber;
            }
            set {
                _employee.HouseNumber = value;
            }
        }
        public string PostalCode { 
            get {
                return _employee.PostalCode;
            }
            set {
                _employee.PostalCode = value;
            }
        }
        public string City {
            get {
                return _employee.City;
            }
            set {
                _employee.City = value;
            }
        }
        public string Email { 
            get {
                return _employee.Email;
            }
            set {
                _employee.Email = value;
            }
        }
        public string Phone { 
            get {
                return _employee.Phone;
            }
            set {
                _employee.Phone = value;
            }
        }
        public string Password { 
            get {
                return _employee.Password;
            }
            set {
                _employee.Password = value;
            }
        }
        private string PasswordResetToken { 
            get {
                return _employee.PasswordResetToken;
            }
            set {
                _employee.PasswordResetToken = value;
            }
        }
        public DateTime ResetTokenEndTime {
            get {
                return (DateTime) _employee.ResetTokenEndTime;
            }
            set {
                _employee.ResetTokenEndTime = value;
            }
        }
        public string Iban { 
            get {
                return _employee.Iban;
            }
            set {
                _employee.Iban = value;
            }
        }
        public string Status {
            get {
                return _employee.Status;
            }
            set {
                _employee.Status = value;
            }
        }

        private Employee _employee;
        public EmployeeVM(Employee employee)
        {
            _employee = employee;
            Department = new DepartmentVM(_employee.Department1);
        }

        public EmployeeVM()
        {
            _employee = new Employee();
        }
        
    }
}
