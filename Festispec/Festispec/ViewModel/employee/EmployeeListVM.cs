using Festispec.Domain;
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
    public class EmployeeListVM
    {
        public ICommand OpenAddEmployee { get; set; }
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
                _selectedEmployee = value;
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
        }

        private void OpenAddEmployeePage()
        {
            _mainViewModel.OpenAddEmployeeTab();
        }
    }
}
