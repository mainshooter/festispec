using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Festispec.View;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand ShowAddEmployeeCommand { get; set; }

        public MainViewModel()
        {
            ShowAddEmployeeCommand = new RelayCommand(ShowAddEmployee);
        }

        public void ShowAddEmployee()
        {
            AddEmployee addEmployeeWindow = new AddEmployee();
            addEmployeeWindow.Show();
        }
    }
}