using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class AddEmployeeVM : ViewModelBase
    {
        public EmployeeListVM EmployeeList { get; set; }
        public EmployeeVM Employee { get; set; }
        public ObservableCollection<DepartmentVM> Departments { get; set; }
        public ObservableCollection<string> Statuses { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand CloseAddEmployeeCommand { get; set; }

        public AddEmployeeVM(EmployeeListVM employeeList)
        {
            EmployeeList = employeeList;
            Employee = new EmployeeVM();
            AddEmployeeCommand = new RelayCommand<PasswordBox>(AddEmployee, CanAddEmployee);
            CloseAddEmployeeCommand = new RelayCommand(CloseAddEmployee);

            using (var context = new Entities())
            {
                Departments = new ObservableCollection<DepartmentVM>(context.Departments.ToList().Select(department => new DepartmentVM(department)));
                Statuses = new ObservableCollection<string>(context.EmployeeStatus.ToList().Select(status => status.Status));
            }

            Employee.Department = Departments.First();
        }

        private void Encrypt()
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Employee.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            Employee.Password = hash;
        }

        public void AddEmployee(PasswordBox password)
        {
            Employee.Password = password.Password;
            Encrypt();

            using (var context = new Entities())
            {
                Employee.DepartmentModel = null;
                context.Employees.Add(Employee.ToModel());
                context.SaveChanges();
            }

            EmployeeList.EmployeeList.Add(Employee);
            EmployeeList.RaisePropertyChanged("EmployeeListFiltered");
            CloseAddEmployee();
        }

        private void CloseAddEmployee()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePage) });
        }

        public bool CanAddEmployee(PasswordBox password)
        {
            if (Employee.Firstname == null || Employee.Lastname == null || password.Password == null || password.Password.Equals("") || Employee.City == null || Employee.Department == null || Employee.Email == null || Employee.HouseNumber <= 0 || Employee.Phone == null || Employee.PostalCode == null || Employee.Status == null || Employee.Street == null || Employee.Birthday.ToString().Equals("") || Employee.Birthday == null || Employee.Iban == null)
            {
                return false;
            }

            string regexEmail = "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            string regexPhone = "^\\s*(?:\\+?(\\d{1,3}))?([-. (]*(\\d{3})[-. )]*)?((\\d{3})[-. ]*(\\d{2,4})(?:[-.x ]*(\\d+))?)\\s*$";
            string regexIban = "[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}";

            if (Employee.Prefix != null)
            {
                if (Employee.Prefix.Length > 45)
                {
                    return false;
                }
            }

            if (!Regex.IsMatch(Employee.Email, regexEmail) || !Regex.IsMatch(Employee.Phone, regexPhone) || !Regex.IsMatch(Employee.Iban, regexIban) || Employee.Birthday.Year < 1900 || Employee.HouseNumberAddition.Length > 5 || Employee.Iban.Length > 27 || Employee.Password.Length > 255 || Employee.Email.Length > 100 || Employee.Phone.Length > 15 || Employee.City.Length > 45 || Employee.PostalCode.Length > 6 || Employee.HouseNumber > 9999 || Employee.Street.Length > 100 || Employee.Lastname.Length > 45 || Employee.Firstname.Length > 45)
            {
                return false;
            }

            return true;
        }
    }
}
