using Festispec.Domain;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class SickPageVM : ViewModelBase
    {
        public string ShowEventInfo { get; set; }
        public string ShowNoEvent { get; set; }
        public string SickPageButton { get; set; }

        public ICommand SickButtonCommand { get; set; }

        private EmployeeVM _employee { get; set; }
        private PlannedEmployeeVM _plannedEmployee { get; set; }


        public SickPageVM()
        {
            Console.WriteLine("faka");
            _employee = UserSessionVm.Current.Employee;

            using (var context = new Entities())
            {
                var tempPlanning = context.InspectorPlannings.ToList()
                    .Select(e => new PlannedEmployeeVM(e))
                    .Where(e => e.Employee.Id == _employee.Id)
                    .Where(e => e.PlannedStartTime.Date == DateTime.Today)
                    .FirstOrDefault();

                if (tempPlanning != null)
                {
                    _plannedEmployee = tempPlanning;
                    var tempEvent = context.Orders.Include("Event").ToList()
                        .Where(e => e.Id == tempPlanning.OrderId)
                        .FirstOrDefault();

                    if(tempEvent != null)
                    {
                        Console.WriteLine(_plannedEmployee.OrderId);
                        Console.WriteLine(tempEvent.Event.Name);
                        ShowEventInfo = "Visible";
                        ShowNoEvent = "Hidden";
                        SickPageButton = "Ziekmelden";

                        SickButtonCommand = new RelayCommand(GetSick);
                    }
                }
                else
                {
                    Console.WriteLine("Geen ding vandaag");
                }
            }
        }

        private void GetSick()
        {
            using(var context = new Entities())
            {
                var sick = new SickReportInspector();
                sick.Employee = UserSessionVm.Current.Employee.ToModel();
                sick.InspectorPlanningEmployeeId = UserSessionVm.Current.Employee.Id;
                sick.InspectorPlanningOrderId = _plannedEmployee.OrderId;
                sick.InspoctorPlanningDayId = _plannedEmployee.DayId;
                sick.Reason = "Ziekmeldknop niffoooo";

                context.SickReportInspectors.Add(sick);
                context.SaveChanges();
            }
        }
    }
}