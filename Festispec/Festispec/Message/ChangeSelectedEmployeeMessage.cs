using Festispec.ViewModel.employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Message
{
    public class ChangeSelectedEmployeeMessage
    {
        public EmployeeVM Employee { get; set; }
        public EmployeeListVM EmployeeList { get; set; }
    }
}
