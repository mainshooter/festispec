using Festispec.Domain;
using Festispec.ViewModel.customer;
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
        private OrderVM _order;
        private EventVM _eventVM;

        public OrderVM OrderVM 
        { 
            get 
            {
                return _order;
            }
            set 
            {
                _order = value;
                RaisePropertyChanged("OrderVM");
            }
        }

        public EventVM EventVM {
            get {
                return _eventVM;
            }
            set {
                _eventVM = value;
            }
        }

        public EmployeeVM Employee { get; set; }
        public int OrderId => _plannedEmployee.OrderId;

        public string Status {
            get => _plannedEmployee.Status;
            set => _plannedEmployee.Status = value;
        }

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
            using (var context = new Entities())
            {
                var currentOrder = context.Orders.Find(OrderId);
                CustomerVM customer = new CustomerVM(currentOrder.Customer);
                EventVM = new EventVM(currentOrder.Event, customer);
            }
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
