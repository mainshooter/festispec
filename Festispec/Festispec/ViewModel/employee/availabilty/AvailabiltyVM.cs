using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.availabilty
{
    public class AvailabiltyVM
    {
        public int Id { get; set; }
        public EmployeeVM Employee { get; set; }
        public DateTime AvailabiltyStart { get; set; }
        public DateTime AvailabiltyEnd { get; set; }
    }
}
