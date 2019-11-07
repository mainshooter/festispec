using Festispec.Domain;
using Festispec.ViewModel.planning;
using Festispec.ViewModel.planning.plannedEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee
{
    public class SickVM
    {
        public int Id { 
            get {
                return _sick.Id;
            }
            private set {
                _sick.Id = value;
            }
        }
        private EmployeeVM _employee;
        public EmployeeVM Employee {
            get {
                return _employee;
            }
            set {
                _employee = value;
            }
        }
        private PlannedEmployeeVM _day;
        public PlannedEmployeeVM Day {
            get {
                return _day;
            }
            set {
                _day = value;
            }
        }
        public PlannedEmployeeVM PlannedEmployee { get; set; }
        public string Reason {
            get {
                return _sick.Reason;
            }
            set {
                Reason = value;
            }
        }

        private SickReportInspector _sick;
        public SickVM(SickReportInspector sick)
        {
            _sick = sick;
            Employee = new EmployeeVM(sick.Employee);
            PlannedEmployee = new PlannedEmployeeVM(sick.InspectorPlanning);
        }
    }
}
