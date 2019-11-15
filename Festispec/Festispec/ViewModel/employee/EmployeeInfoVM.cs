using Festispec.Message;
using Festispec.View.Pages.Employee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class EmployeeInfoVM : ViewModelBase
    {
        private EmployeeVM _employee;

        public ICommand CloseSingleEmployeeCommand { get; set; }
        public EmployeeVM Employee {
            get {
                return _employee;
            }
            set {
                _employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        public EmployeeInfoVM()
        {
            CloseSingleEmployeeCommand = new RelayCommand(CloseSingleEmployee);
            this.MessengerInstance.Register<ChangeSelectedEmployeeMessage>(this, message =>
            {
                Employee = message.Employee;
            });
        }

        public void CloseSingleEmployee()
        {
            this.MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePage) });
        }
    }
}
