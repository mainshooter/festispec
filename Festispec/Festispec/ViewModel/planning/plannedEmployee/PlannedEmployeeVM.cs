using Festispec.Domain;
using Festispec.ViewModel.customer.customerEvent;
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
        private DateTime _plannedDate;

        public EmployeeVM Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                if (value != null)
                {
                    _plannedEmployee.EmployeeId = value.Id;
                }

                RaisePropertyChanged(() => Employee);
            }
        }

        public DateTime PlannedDate
        {
            get => _plannedDate;
            set
            {
                _plannedDate = value;
                PlannedStartTime = value;
                PlannedEndTime = value;
                Employee = null;
                RaisePropertyChanged(() => PlannedDate);
            }
        }

        public int OrderId => _plannedEmployee.OrderId;

        public OrderVM Order
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
                if (value < Day.EndTime && PlannedStartTime >= Day.BeginTime)
                {
                    PlannedStartTime = Day.EndTime;
                    return;
                }
                if (value < Day.BeginTime)
                {
                    PlannedStartTime = Day.BeginTime;
                    return;
                }
                if (value > PlannedEndTime)
                {
                    PlannedEndTime = value;
                }
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
                if (value > Day.EndTime)
                {
                    PlannedEndTime = Day.EndTime;
                    return;
                }
                if (value < PlannedStartTime)
                {
                    PlannedEndTime = PlannedStartTime;
                    return;
                }
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

        public PlannedEmployeeVM(InspectorPlanning pe, DayVM dayVM)
        {
            _plannedEmployee = pe;
            Employee = new EmployeeVM(pe.Employee);
            Day = dayVM;
        }

        public PlannedEmployeeVM(DayVM dayVM)
        {
            _plannedEmployee = new InspectorPlanning();
            Day = dayVM;
        }

        public InspectorPlanning ToModel()
        {
            return _plannedEmployee;
        }
    }
}
