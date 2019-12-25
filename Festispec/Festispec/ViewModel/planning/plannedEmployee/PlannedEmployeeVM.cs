using Festispec.Domain;
using Festispec.ViewModel.employee;
using GalaSoft.MvvmLight;
using System;

namespace Festispec.ViewModel.planning.plannedEmployee
{
    public class PlannedEmployeeVM : ViewModelBase
    {
        private InspectorPlanning _plannedEmployee;

        public EmployeeVM Employee { get; set; }
        public int OrderId => _plannedEmployee.OrderId;

        public string Status 
        {
            get => _plannedEmployee.Status;
            set => _plannedEmployee.Status = value;
        }

        public DateTime PlannedStartTime 
        {
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

        public DateTime PlannedEndTime 
        {
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

        public string EventName { get; set; }

        public string EventStreet { get; set; }

        public int EventHouseNumber { get; set; }

        public string EventHouseNumberAddition { get; set; }

        public string EventLocation 
        { 
            get 
            {
                return EventStreet + " " + EventHouseNumber + EventHouseNumberAddition;
            }
        }

        public string EventCity { get; set; }

        public int DayId => _plannedEmployee.DayId;

        public PlannedEmployeeVM(InspectorPlanning pe)
        {
            _plannedEmployee = pe;
            using (var context = new Entities())
            {
                var currentOrder = context.Orders.Find(OrderId);
                var currentEvent = currentOrder.Event;
                EventName = currentEvent.Name;
                EventStreet = currentEvent.Street;
                EventHouseNumber = currentEvent.HouseNumber;
                EventHouseNumberAddition = currentEvent.HouseNumber_Addition;
                EventCity = currentEvent.City;
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
