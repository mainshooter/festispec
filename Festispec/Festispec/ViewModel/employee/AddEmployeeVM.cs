using Festispec.Domain;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class AddEmployeeVM
    {
        public EmployeeListVM EmployeeList { get; set; }
        public EmployeeVM Employee { get; set; }
        public ObservableCollection<DepartmentVM> Departments { get; set; }
        public ObservableCollection<string> Status { get; set; }
        public ICommand AddEmployeeCommand { get; set; }

        public AddEmployeeVM(EmployeeListVM employeeList)
        {
            EmployeeList = employeeList;
            Employee = new EmployeeVM();
            AddEmployeeCommand = new RelayCommand(AddEmployee, CanAddEmployee);
            using (var context = new Entities())
            {
                Departments = new ObservableCollection<DepartmentVM>(context.Departments.ToList().Select(department => new DepartmentVM(department)));
                Status = new ObservableCollection<string>(context.EmployeeStatus.ToList().Select(status => status.Status));
            }
        }

        public void AddEmployee()
        {
            using (var context = new Entities())
            {
                context.Employees.Add(Employee.ToModel());
                context.SaveChanges();
            }
            EmployeeList.CloseAddEmployee();
        }

        public bool CanAddEmployee()
        {
            if (Employee.Firstname == null || Employee.Lastname == null || Employee.Password == null || Employee.City == null || Employee.Department == null || Employee.Email == null || Employee.HouseNumber <= 0 || Employee.Phone == null || Employee.PostalCode == null || Employee.Status == null || Employee.Street == null)
            {
                return false;
            }

            //todo validatie phone, iban, email, huisnummer
            return true;
        }
    }
}
