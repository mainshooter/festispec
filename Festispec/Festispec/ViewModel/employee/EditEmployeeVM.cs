using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class EditEmployeeVM : ViewModelBase
    {
        private EmployeeVM _employeeVM;
        private int _departmentIndex;

        public EmployeeListVM EmployeeList { get; set; }
        public ObservableCollection<DepartmentVM> Departments { get; set; }
        public ObservableCollection<string> Statuses { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand CloseEditEmployeeCommand { get; set; }

        public EmployeeVM Employee
        {
            get
            {
                return _employeeVM;
            }
            set
            {
                _employeeVM = value;
                RaisePropertyChanged();
            }
        }

        public int DepartmentIndex
        {
            get
            {
                if (Employee != null)
                {
                    return Departments.IndexOf(Departments.Select(department => department).Where(department => department.Name == Employee.Department.Name).FirstOrDefault());
                }
                return 0;
            }
        }

        public EditEmployeeVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEmployeeMessage>(this, message =>
            {
                EmployeeList = message.EmployeeList;
                Employee = message.Employee;
                RaisePropertyChanged("DepartmentIndex");
            });
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditEmployee);
            CloseEditEmployeeCommand = new RelayCommand(CloseEditEmployee);

            using (var context = new Entities())
            {
                Departments = new ObservableCollection<DepartmentVM>(context.Departments.ToList().Select(department => new DepartmentVM(department)));
                Statuses = new ObservableCollection<string>(context.EmployeeStatus.ToList().Select(status => status.Status));
            }
            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(EmployeePage) && EmployeeList != null)
                {
                    EmployeeList.RefreshEmployees();
                }
            });
        }

        public void EditEmployee()
        {
            using (var context = new Entities())
            {
                context.Entry(Employee.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            CloseEditEmployee();
        }

        private void CloseEditEmployee()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePage) });
        }

        public bool CanEditEmployee()
        {
            if (Employee == null)
            {
                return false;
            }
            return Employee.IsValid;
        }
    }
}
