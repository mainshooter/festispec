﻿using Festispec.Domain;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.planning.plannedEmployee;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.planning
{
    public class DayVM
    {
        private ObservableCollection<PlannedEmployeeVM> _inspectorPlannings;
        private Day _day;

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

        public DayVM(Day day)
        {
            _day = day;
            //Order = new OrderVM(day.Order);
            //InspectorPlannings = new ObservableCollection<PlannedEmployeeVM>(day.InspectorPlannings.ToList().Select(i => new PlannedEmployeeVM(i)));
        }

        public DayVM()
        {
            _day = new Day();
        }
    }
}
