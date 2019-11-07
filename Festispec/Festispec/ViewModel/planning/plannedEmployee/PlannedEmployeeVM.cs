using Festispec.Domain;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.employee.assignment;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.employee.quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.planning.plannedEmployee
{
    public class PlannedEmployeeVM
    {
        public EmployeeVM Employee { get; set; }
        public DayVM Day { get; set; }
        public OrderVM Order { get; set; }
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
        public DateTime PlannedEndTime {
            get {
                return _plannedEmployee.PlannedTill;
            }
            set {
                _plannedEmployee.PlannedTill = value;
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

        private InspectorPlanning _plannedEmployee;
        public PlannedEmployeeVM(InspectorPlanning pe)
        {
            _plannedEmployee = pe;
            Employee = new EmployeeVM(pe.Employee);
            Day = new DayVM(pe.Day);
        }
    }
}
