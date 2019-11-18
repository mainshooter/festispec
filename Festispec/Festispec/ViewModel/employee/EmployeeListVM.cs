using Festispec.Domain;
using Festispec.Message;
using Festispec.Repository;
using Festispec.View.Pages.Employee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class EmployeeListVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filters;
        private EmployeeVM _selectedEmployee;

        public ICommand OpenAddEmployee { get; set; }
        public ICommand OpenEditEmployee { get; set; }
        public ICommand OpenSingleEmployee { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ObservableCollection<EmployeeVM> EmployeeList { get; set; }
        public string SelectedFilter { get; set; }

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                RaisePropertyChanged("EmployeeListFiltered");
            }
        }

        public List<string> Filters
        {
            get => _filters;
            set
            {
                _filters = new List<string>();
                _filters.Add("Voornaam");
                _filters.Add("Achternaam");
                _filters.Add("Plaats");
                _filters.Add("E-mail");
                _filters.Add("Afdeling");
                _filters.Add("Status");
            }
        }

        public ObservableCollection<EmployeeVM> EmployeeListFiltered
        {
            get
            {
                if (Filter != null || !Filter.Equals(""))
                {
                    switch (SelectedFilter)
                    {
                        case "Voornaam":
                            return new ObservableCollection<EmployeeVM>(EmployeeList.Select(employee => employee).Where(employee => employee.Firstname.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Achternaam":
                            return new ObservableCollection<EmployeeVM>(EmployeeList.Select(employee => employee).Where(employee => employee.Lastname.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Plaats":
                            return new ObservableCollection<EmployeeVM>(EmployeeList.Select(employee => employee).Where(employee => employee.City.ToLower().Contains(Filter.ToLower())).ToList());
                        case "E-mail":
                            return new ObservableCollection<EmployeeVM>(EmployeeList.Select(employee => employee).Where(employee => employee.Email.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Afdeling":
                            return new ObservableCollection<EmployeeVM>(EmployeeList.Select(employee => employee).Where(employee => employee.Department.Name.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Status":
                            return new ObservableCollection<EmployeeVM>(EmployeeList.Select(employee => employee).Where(employee => employee.Status.ToLower().Contains(Filter.ToLower())).ToList());
                    }
                }

                return EmployeeList;
            }
        }

        public EmployeeVM SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (value != null)
                {
                    _selectedEmployee = value;
                    RaisePropertyChanged();
                }
            }
        }
        public EmployeeListVM()
        {
            Filters = new List<string>();
            SelectedFilter = Filters.First();
            Filter = "";
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeList = new ObservableCollection<EmployeeVM>(employeeRepository.GetEmployees());
            OpenAddEmployee = new RelayCommand(OpenAddEmployeePage);
            OpenEditEmployee = new RelayCommand(OpenEditEmployeePage);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
            OpenSingleEmployee = new RelayCommand(OpenSingleEmployeePage);
        }

        private void OpenAddEmployeePage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddEmployeePage) });
        }

        private void OpenEditEmployeePage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditEmployeePage) });
            MessengerInstance.Send<ChangeSelectedEmployeeMessage>(new ChangeSelectedEmployeeMessage()
            {
                Employee = SelectedEmployee,
                EmployeeList = this
            });
        }

        public void OpenSingleEmployeePage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SingleEmployeePage) });
            MessengerInstance.Send<ChangeSelectedEmployeeMessage>(new ChangeSelectedEmployeeMessage() { Employee = SelectedEmployee });

        }

        private void DeleteEmployee()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze medewerker wilt verwijderen?", "Medewerker Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    var temp = SelectedEmployee.ToModel();
                    context.Employees.Remove(context.Employees.Select(employee => employee).Where(employee => employee.Id == temp.Id).First());
                    context.SaveChanges();
                }
                EmployeeList.Remove(SelectedEmployee);
                RaisePropertyChanged("EmployeeListFiltered");
            }
        }

        public void RefreshEmployees()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeList = new ObservableCollection<EmployeeVM>(employeeRepository.GetEmployees());
            RaisePropertyChanged("EmployeeListFiltered");
        }
    }
}
