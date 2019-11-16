using Festispec.ViewModel.employee;

namespace Festispec.Message
{
    public class ChangeSelectedEmployeeMessage
    {
        public EmployeeVM Employee { get; set; }
        public EmployeeListVM EmployeeList { get; set; }
    }
}
