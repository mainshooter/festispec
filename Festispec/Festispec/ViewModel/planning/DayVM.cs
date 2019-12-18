using Festispec.Domain;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.planning.plannedEmployee;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.planning
{
    public class DayVM
    {
        private Day _day;

        public int Id => _day.Id;

        public OrderVM Order { get; set; }

        public ObservableCollection<PlannedEmployeeVM> InspectorPlannings { get; set; }

        public DateTime BeginTime
        {
            get
            {
                return _day.BeginTime;
            }
            set
            {
                _day.BeginTime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return _day.EndTime;
            }
            set
            {
                _day.EndTime = value;
            }
        }

        public DayVM(Day day, OrderVM orderVM)
        {
            _day = day;
            Order = orderVM;
            InspectorPlannings = new ObservableCollection<PlannedEmployeeVM>(day.InspectorPlannings.ToList().Select(ip => new PlannedEmployeeVM(ip, this)));
        }
    }
}
