using Festispec.Domain;
using Festispec.ViewModel.employee;
using System;

namespace Festispec.ViewModel.planning.plannedEmployee
{
    public class PlannedEmployeeVM
    {
        private InspectorPlanning _plannedEmployee;

        public EmployeeVM Employee { get; set; }

        public DayVM Day { get; set; }

        public string Status {
            get {
                return _plannedEmployee.Status;
            }
            set {
                _plannedEmployee.Status = value;
            }
        }

        public DateTime PlannedStartTime {
            get {
                return _plannedEmployee.PlannedFrom;
            }
            set {
                _plannedEmployee.PlannedFrom = value;
            }
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
            get {
                return _plannedEmployee.PlannedTill;
            }
            set {
                _plannedEmployee.PlannedTill = value;
            }
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
            get {
                return (DateTime) _plannedEmployee.WorkedFrom;
            }
            set {
                _plannedEmployee.WorkedFrom = value;
            }
        }

        public DateTime WorkEndTime {
            get {
                return (DateTime)_plannedEmployee.WorkedTill;
            }
            set {
                _plannedEmployee.WorkedTill = value;
            }
        }

        public PlannedEmployeeVM(InspectorPlanning pe)
        {
            _plannedEmployee = pe;
            Employee = new EmployeeVM(pe.Employee);
            Day = new DayVM(pe.Day);
        }

        public PlannedEmployeeVM()
        {
            _plannedEmployee = new InspectorPlanning();
        }
    }
}
