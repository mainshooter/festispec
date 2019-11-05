using Festispec.ViewModel.planning.plannedEmployee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.planning
{
    public class PlanningVM
    {
        public ObservableCollection<PlannedEmployeeVM> PlannedEmployees { get; set; }
    }
}
