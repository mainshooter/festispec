using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using Festispec.ViewModel.toast;

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
            var passwordService = new PasswordHashService();
            Employee.Password = passwordService.StringToPassword(Employee.Password);
        }

        public void AddEmployee(PasswordBox password)
        {
            using (var context = new Entities())
            {
                var employees = new List<Employee>(context.Employees);
                if (employees.Select(employee => employee.Email).Where(email => email == Employee.Email).Count() > 0)
                {
                    var toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
                    toast.ShowError("Een gebruiker met dit emailadres bestaat al");
                    return;
                }
            }
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
            Employee = new EmployeeVM();
            Employee.Department = Departments.First();
            Employee.Status = Statuses.First();
            RaisePropertyChanged("Employee");
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePage) });
        }

        public bool CanAddEmployee(PasswordBox password)
        {
            if (password != null)
            {
                Employee.Password = password.Password;
                return Employee.IsValid;
            }
            return false;
        }
    }
}
