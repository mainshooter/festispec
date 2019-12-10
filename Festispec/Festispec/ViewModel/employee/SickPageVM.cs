using Festispec.Domain;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
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
                List<Order> orders = new List<Order>(context.Orders);
                List<OrderVM> orderList = new List<OrderVM>(context.Orders.ToList().Select(e => new OrderVM(e)));
                if (tempPlanning != null)
                {
                    
                    var tempEvent = orderList.Where(e => e.Id == tempPlanning.OrderId).FirstOrDefault();

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
