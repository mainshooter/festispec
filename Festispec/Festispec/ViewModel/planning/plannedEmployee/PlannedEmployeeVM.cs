using Festispec.Domain;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.employee;
using GalaSoft.MvvmLight;
using System;

namespace Festispec.ViewModel.planning.plannedEmployee
{
    public class PlannedEmployeeVM : ViewModelBase
    {
        private InspectorPlanning _plannedEmployee;
        private DayVM _day;
        private EmployeeVM _employee;
        private OrderVM _order;

        public OrderVM OrderVM
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
                _plannedEmployee.OrderId = value.Id;
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
                _plannedEmployee.EmployeeId = value.Id;
            }
        }
        public EmployeeVM Employee { get; set; }
        public int OrderId => _plannedEmployee.OrderId;

        public DayVM Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;
                _plannedEmployee.DayId = value.Id;
            }
        }

        public DateTime PlannedStartTime
        {
            get => _plannedEmployee.PlannedFrom;
            set
            {
                _plannedEmployee.PlannedFrom = value;
                RaisePropertyChanged(() => PlannedStartTime);
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

        public DateTime PlannedEndTime
        {
            get => _plannedEmployee.PlannedTill;
            set
            {
                _plannedEmployee.PlannedTill = value;
                RaisePropertyChanged(() => PlannedEndTime);
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

        public DateTime WorkStartTime
        {
            get => (DateTime)_plannedEmployee.WorkedFrom;
            set => _plannedEmployee.WorkedFrom = value;
        }

        public DateTime WorkEndTime
        {
            get => (DateTime)_plannedEmployee.WorkedTill;
            set => _plannedEmployee.WorkedTill = value;
        }

        public int DayId => _plannedEmployee.DayId;

        public PlannedEmployeeVM(InspectorPlanning pe)
        {
            _plannedEmployee = pe;
            Employee = new EmployeeVM(pe.Employee);
        }

        public PlannedEmployeeVM(InspectorPlanning pe, EmployeeVM employee)
        {
            _plannedEmployee = pe;
            Employee = employee;
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
