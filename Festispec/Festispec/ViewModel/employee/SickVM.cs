﻿using Festispec.Domain;
using Festispec.ViewModel.planning.plannedEmployee;

namespace Festispec.ViewModel.employee
{
    public class SickVM
    {
        private EmployeeVM _employee;
        private PlannedEmployeeVM _day;
        private SickReportInspector _sick;

        public int Id 
        { 
            get 
            {
                return _sick.Id;
            }
            private set 
            {
                _sick.Id = value;
            }
        }
        
        public EmployeeVM Employee 
        {
            get 
            {
                return _employee;
            }
            set 
            {
                _employee = value;
            }
        }
        
        public PlannedEmployeeVM Day 
        {
            get 
            {
                return _day;
            }
            set 
            {
                _day = value;
            }
        }

        public PlannedEmployeeVM PlannedEmployee { get; set; }

        public string Reason 
        {
            get 
            {
                return _sick.Reason;
            }
            set 
            {
                Reason = value;
            }
        }

        public SickVM(SickReportInspector sick)
        {
            _sick = sick;
            Employee = new EmployeeVM(sick.Employee);
            PlannedEmployee = new PlannedEmployeeVM(sick.InspectorPlanning);
        }

        public SickVM()
        {
            _sick = new SickReportInspector();
        }
    }
}
