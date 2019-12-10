using Festispec.Domain;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System;
using System.Linq;

namespace Festispec.ViewModel.employee
{
    public class SickPageVM : ViewModelBase
    {
        private EmployeeVM Employee { get; set; }
        private PlannedEmployeeVM PlannedEvent { get; set; }
        private OrderVM PlannedOrder { get; set; }

        public SickPageVM()
        {
            Console.WriteLine("faka");
            Employee = UserSessionVm.Current.Employee;

            using (var context = new Entities())
            {
                var tempPlanning = context.InspectorPlannings.ToList()
                    .Select(e => new PlannedEmployeeVM(e))
                    .Where(e => e.Employee.Id == Employee.Id)
                    .FirstOrDefault();

                var tempEvent = context.Orders.ToList().Select(e => new OrderVM(e)).Where(e => e.Id == tempPlanning.OrderId).FirstOrDefault();
                if (tempPlanning != null)
                {
                    PlannedEvent = tempPlanning;
                    Console.WriteLine(PlannedEvent.OrderId);
                    Console.WriteLine(tempEvent.Event.Name);
                }
                else
                {
                    Console.WriteLine("Geen ding vandaag");
                }
            }
        }
    }
}
