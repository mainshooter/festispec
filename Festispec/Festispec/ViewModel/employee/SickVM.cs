using Festispec.ViewModel.planning;
using Festispec.ViewModel.planning.plannedEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee
{
    public class SickVM
    {
        public int Id { get; set; }
        public EmployeeVM Employee { get; set; }
        public DayVM Day { get; set; }
        public PlannedEmployeeVM PlannedEmployee { get; set; }
        public string Reason { get; set; }
    }
}
