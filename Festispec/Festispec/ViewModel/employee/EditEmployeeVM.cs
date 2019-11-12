using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class EditEmployeeVM
    {
        public EmployeeListVM EmployeeList { get; set; }
        public EmployeeVM Employee { get; set; }
        public ObservableCollection<DepartmentVM> Departments { get; set; }
        public ObservableCollection<string> Status { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand CloseEditEmployeeCommand { get; set; }

        public EditEmployeeVM(EmployeeListVM employeeList)
        {
            EmployeeList = employeeList;
            Employee = EmployeeList.SelectedEmployee;
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditEmployee);
            CloseEditEmployeeCommand = new RelayCommand(CloseEditEmployee);
            using (var context = new Entities())
            {
                Departments = new ObservableCollection<DepartmentVM>(context.Departments.ToList().Select(department => new DepartmentVM(department)));
                Status = new ObservableCollection<string>(context.EmployeeStatus.ToList().Select(status => status.Status));
            }
        }

        public void EditEmployee()
        {
            using (var context = new Entities())
            {
                context.Entry(Employee.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            EmployeeList.CloseTab();
        }

        private void CloseEditEmployee()
        {
            EmployeeList.CloseTab();
        }

        public bool CanEditEmployee()
        {
            if (Employee.Firstname == null || Employee.Lastname == null || Employee.Password == null || Employee.City == null || Employee.Department == null || Employee.Email == null || Employee.HouseNumber <= 0 || Employee.Phone == null || Employee.PostalCode == null || Employee.Status == null || Employee.Street == null)
            {
                return false;
            }
            string regexEmail = "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            string regexPhone = "^\\s*(?:\\+?(\\d{1,3}))?([-. (]*(\\d{3})[-. )]*)?((\\d{3})[-. ]*(\\d{2,4})(?:[-.x ]*(\\d+))?)\\s*$";
            string regexIban = "[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}";
            if (!Regex.IsMatch(Employee.Email, regexEmail) || !Regex.IsMatch(Employee.Phone, regexPhone) || !Regex.IsMatch(Employee.Iban, regexIban))
            {
                return false;
            }
            return true;
        }
    }
}
