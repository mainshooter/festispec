using Festispec.Domain;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.planning;
using Festispec.ViewModel.report;
using System.Collections.ObjectModel;
using System.Linq;
using Festispec.ViewModel.survey;

namespace Festispec.ViewModel.Customer.order
{
    public class OrderVM
    {
        private Order _order;

        public int Id => _order.Id;
        public EmployeeVM Employee { get; set; }
        public EventVM Event { get; set; }
        public ObservableCollection<DayVM> Days { get; set; }
        public ReportVM Report { get; set; }
        public SurveyVM Survey { get; set; }

        public string Status
        { 
            get => _order.Status;
            set => _order.Status = value;
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
            Survey = orderCon.Surveys.Count > 0 ? new SurveyVM(this, orderCon.Surveys.First()) : new SurveyVM(this);
            Days = new ObservableCollection<DayVM>(_order.Days.ToList().Select(d => new DayVM(d, this)));
        }

        public OrderVM()
        {
            _order = new Order();
        }

        public OrderVM(Order e)
        {
            _order = e;
        }

        public Order ToModel()
        {
            return _order;
        }
    }
}
