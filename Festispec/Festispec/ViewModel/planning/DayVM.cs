using Festispec.ViewModel.employee.assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.planning
{
    public class DayVM
    {
        public int Id { get; set; }
        public AssignmentVM Assignment { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
