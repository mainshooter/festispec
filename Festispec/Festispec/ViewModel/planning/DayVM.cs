using Festispec.Domain;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.planning.plannedEmployee;
using System.Collections.ObjectModel;

namespace Festispec.ViewModel.planning
{
    public class DayVM
    {
        private Day _day;

        public int Id => _day.Id;
        public OrderVM Order { get; set; }
        public ObservableCollection<PlannedEmployeeVM> InspectorPlannings { get; set; }

        public DayVM(Day day, OrderVM orderVM)
        {
            _day = day;
            Order = orderVM;
        }

        public DayVM()
        {
            _day = new Day();
        }
    }
}
