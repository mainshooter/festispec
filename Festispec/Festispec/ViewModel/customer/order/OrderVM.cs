using Festispec.Domain;
using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.planning;
using Festispec.ViewModel.rapport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.order
{
    public class OrderVM
    {
        private Order _order;
        private EventVM _event;
        private CustomerVM _customer;
        private EmployeeVM _employee;
        private ObservableCollection<ReportVM> _reports;

        public int Id { 
            get {
                return _order.Id;
            }
            private set {
                _order.Id = value;
            }
        }
        
        public CustomerVM Customer { 
            get {
                return _customer;
            }
            set {
                _customer = value;
            }
        }
        
        public EventVM Event { 
            get {
                return _event;
            }
            set {
                _event = value;
            }
        }
        
        public EmployeeVM Employee { 
            get {
                return _employee;
            }
            set {
                _employee = value;
            }
        }
        public ObservableCollection<DayVM> Days { get; set; }

        public string Status { 
            get {
                return _order.Status;
            }
            set {
                _order.Status = value;
            }
        }

        public string Description { 
            get {
                return _order.Description;
            }
            set {
                _order.Description = value;
            }
        }
        
        public ObservableCollection<ReportVM> Reports {
            get {
                return _reports;
            }
            set {
                _reports = value;
            }
        }

        public OrderVM(Order orderCon)
        {
            _order = orderCon;
            Customer = new CustomerVM(orderCon.Customer);
            Event = new EventVM(orderCon.Event);
            Employee = new EmployeeVM(orderCon.Employee);
            //Days = new ObservableCollection<DayVM>(_order.Days.ToList().Select(d => new DayVM(d)));
            //Reports = new ObservableCollection<ReportVM>(_order.Reports.ToList().Select(r => new ReportVM(r)));
        }

        public OrderVM()
        {
            _order = new Order();
        }
    }
}
