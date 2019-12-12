using Festispec.Domain;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.planning;
using Festispec.ViewModel.report;
using System.Collections.ObjectModel;
using System.Linq;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.employee.quotation;
using Festispec.ViewModel.customer;

namespace Festispec.ViewModel.Customer.order
{
    public class OrderVM
    {
        private Order _order;
        private QuotationVM _quotation;
        private EventVM _event;
        private EmployeeVM _employee;
        private CustomerVM _customer;

        public int Id => _order.Id;
        public ObservableCollection<DayVM> Days { get; set; }
        public ReportVM Report { get; set; }
        public SurveyVM Survey { get; set; }
        
        public QuotationVM Quotation 
        {
            get
            {
                return _quotation;
            }
            set
            {
                _quotation = value;
                _order.QuotationId = value.Id;
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
                _order.EmployeeId = value.Id;
            }
        }

        public EventVM Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                _order.EventId = value.Id;
            }
        }

        public CustomerVM Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                _order.CustomerId = value.Id;
            }
        }

        public string Description
        { 
            get => _order.Description;
            set => _order.Description = value;
        }

        public OrderVM(Order orderCon, EventVM eventVM)
        {
            _order = orderCon;
            Event = eventVM;
            Employee = new EmployeeVM(orderCon.Employee);
            Customer = eventVM.Customer;
            Survey = orderCon.Surveys.Count > 0 ? new SurveyVM(this, orderCon.Surveys.First()) : new SurveyVM(this);
            Days = new ObservableCollection<DayVM>(_order.Days.ToList().Select(d => new DayVM(d, this)));
        }

        public OrderVM()
        {
            _order = new Order();
        }

        public Order ToModel()
        {
            return _order;
        }
    }
}
