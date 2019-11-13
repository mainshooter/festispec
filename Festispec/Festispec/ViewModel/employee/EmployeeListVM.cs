using Festispec.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class EmployeeListVM : ViewModelBase
    {
        public ICommand OpenAddEmployee { get; set; }
        public ICommand OpenEditEmployee { get; set; }
        public ICommand OpenSingleEmployee { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        private MainViewModel _mainViewModel;
        public ObservableCollection<EmployeeVM> EmployeeList { get; set; }
        private EmployeeVM _selectedEmployee;
        public MessageBox MessageBox { get; set; }
        private string _filter;
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                RaisePropertyChanged("EmployeeListFiltered");
            }
        }

        public string SelectedFilter { get; set; }
        private List<string> _filters;
        public List<string> Filters
        {
            get
            {
                return _filters;
            }
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
            get
            {
                return _selectedEmployee;
            }
            set
            {
                if (value != null)
                {
                    _selectedEmployee = value;
                    RaisePropertyChanged();
                }
            }
        }


        public EmployeeListVM(MainViewModel mainViewModel)
        {
            Filters = new List<string>();
            SelectedFilter = Filters.First();
            Filter = "";
            _mainViewModel = mainViewModel;
            using (var context = new Entities())
            {
                EmployeeList = new ObservableCollection<EmployeeVM>(context.Employees.ToList().Select(employee => new EmployeeVM(employee)));
            }
            OpenAddEmployee = new RelayCommand(OpenAddEmployeePage);
            OpenEditEmployee = new RelayCommand(OpenEditEmployeePage);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
            OpenSingleEmployee = new RelayCommand(OpenSingleEmployeePage);
        }

        private void OpenAddEmployeePage()
        {
            _mainViewModel.OpenAddEmployeeTab();
        }

        private void OpenEditEmployeePage()
        {
            _mainViewModel.OpenEditEmployeeTab();
        }

        public void OpenSingleEmployeePage()
        {
            _mainViewModel.OpenSingleEmployeeTab();
        }

        public void CloseTab()
        {
            _mainViewModel.OpenEmployeeTab();
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
                RaisePropertyChanged("EmployeeList");
            }
        }
    }
}
