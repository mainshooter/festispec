using Festispec.Domain;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.planning.plannedEmployee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.planning
{
    public class DayVM
    {
        private ObservableCollection<PlannedEmployeeVM> _inspectorPlannings;
        private Day _day;

        public DayVM(Day day)
        {
            _day = day;
            Order = new OrderVM(day.Order);
            InspectorPlannings = new ObservableCollection<PlannedEmployeeVM>(day.InspectorPlannings.ToList().Select(i => new PlannedEmployeeVM(i)));
        }

        public DayVM()
        {
            _day = new Day();
        }

        public int Id {
            get {
                return _day.Id;
            }
            private set {
                _day.Id = value;
            }
        }
        public OrderVM Order { get; set; }

        
        public ObservableCollection<PlannedEmployeeVM> InspectorPlannings {
            get {
                return _inspectorPlannings;
            }
            set {
                _inspectorPlannings = value;
            }
        }
    }
}
