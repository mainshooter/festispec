using Festispec.Domain;
using GalaSoft.MvvmLight;
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
    public class EmployeeListVM : ViewModelBase
    {
        public ICommand OpenAddEmployee { get; set; }
        public ICommand OpenEditEmployee { get; set; }
        private MainViewModel _mainViewModel;
        public ObservableCollection<EmployeeVM> EmployeeList { get; set; }
        private EmployeeVM _selectedEmployee;
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
            _mainViewModel = mainViewModel;
            using (var context = new Entities())
            {
                EmployeeList = new ObservableCollection<EmployeeVM>(context.Employees.ToList().Select(employee => new EmployeeVM(employee)));
            }
            OpenAddEmployee = new RelayCommand(OpenAddEmployeePage);
            OpenEditEmployee = new RelayCommand(OpenEditEmployeePage);
        }

        private void OpenAddEmployeePage()
        {
            _mainViewModel.OpenAddEmployeeTab();
        }

        private void OpenEditEmployeePage()
        {
            _mainViewModel.OpenEditEmployeeTab();
        }

        public void CloseAddEmployee()
        {
            _mainViewModel.OpenEmployeeTab();
        }

        public void CloseEditEmployee()
        {
            _mainViewModel.OpenEmployeeTab();
        }
    }
}
