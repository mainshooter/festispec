using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.planning.plannedEmployee;
using System.Collections.ObjectModel;

namespace Festispec.Message
{
    public class ChangeSelectedPlannedEmployeeMessage
    {
        public PlannedEmployeeVM PlannedEmployee { get; set; }
        public EventVM EventVM { get; set; }
    }
}
