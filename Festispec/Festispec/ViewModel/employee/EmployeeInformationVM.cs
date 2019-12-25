using Festispec.ViewModel.auth;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.employee
{
    public class EmployeeInformationVM : ViewModelBase
    {
        public EmployeeVM Employee { get; set; }

        public EmployeeInformationVM()
        {
            Employee = UserSessionVM.Current.Employee;
        }
    }
}
