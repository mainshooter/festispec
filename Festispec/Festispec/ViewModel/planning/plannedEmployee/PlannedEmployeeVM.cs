using Festispec.ViewModel.employee;
using Festispec.ViewModel.employee.assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.planning.plannedEmployee
{
    public class PlannedEmployeeVM
    {
        public EmployeeVM Employee { get; set; }
        public DayVM Day { get; set; }
        public AssignmentVM Assignment { get; set; }
        public string Status { get; set; }
        public DateTime PlannedStartTime { get; set; }
        public DateTime PlannedEndTime { get; set; }
        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
    }
}
