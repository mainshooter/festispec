using Festispec.Domain;
using Festispec.ViewModel.employee;
using System;

namespace Festispec.ViewModel.planning.plannedEmployee
{
    public class PlannedEmployeeVM
    {
        private InspectorPlanning _plannedEmployee;

        public EmployeeVM Employee { get; set; }
        public int OrderId => _plannedEmployee.OrderId;

        public DateTime PlannedStartTime {
            get => _plannedEmployee.PlannedFrom;
            set => _plannedEmployee.PlannedFrom = value;
        }

        public string ActualStartDateTime
        {
            get
            {
                var time = PlannedStartTime;
                return Convert.ToDateTime(time).ToString("dd-MM-yyyy HH:mm");
            }
        }

        public DateTime PlannedEndTime {
            get => _plannedEmployee.PlannedTill;
            set => _plannedEmployee.PlannedTill = value;
        }

        public string ActualEndDateTime
        {
            get
            {
                var time = PlannedEndTime;
                return Convert.ToDateTime(time).ToString("dd-MM-yyyy HH:mm");
            }
        }

        public DateTime WorkStartTime {
            get => (DateTime) _plannedEmployee.WorkedFrom;
            set => _plannedEmployee.WorkedFrom = value;
        }

        public DateTime WorkEndTime {
            get => (DateTime)_plannedEmployee.WorkedTill;
            set => _plannedEmployee.WorkedTill = value;
        }

        public int DayId => _plannedEmployee.DayId;

        public PlannedEmployeeVM(InspectorPlanning pe)
        {
            _plannedEmployee = pe;
            Employee = new EmployeeVM(pe.Employee);
        }

        public PlannedEmployeeVM()
        {
            _plannedEmployee = new InspectorPlanning();
        }

        public InspectorPlanning ToModel()
        {
            return _plannedEmployee;
        }
    }
}
