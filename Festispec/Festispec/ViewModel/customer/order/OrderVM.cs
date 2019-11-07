using Festispec.Domain;
using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.order
{
    public class OrderVM
    {
        public int Id { 
            get {
                return _order.Id;
            }
            private set {
                _order.Id = value;
            }
        }
        private CustomerVM _customer;
        public CustomerVM Customer { 
            get {
                return _customer;
            }
            set {
                _customer = value;
            }
        }
        private EventVM _event;
        public EventVM Event { 
            get {
                return _event;
            }
            set {
                _event = value;
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
        public OfferVM Offer { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        private Order _order;
        public OrderVM(Order orderCon)
        {
            _order = orderCon;
            Customer = new CustomerVM(orderCon.Customer);
            Event = new EventVM(orderCon.Event);
            Employee = new EmployeeVM(orderCon.Employee);
        }
    }
}
