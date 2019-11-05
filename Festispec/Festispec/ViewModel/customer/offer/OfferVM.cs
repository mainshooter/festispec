using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.offer
{
    public class OfferVM
    {
        public int Id { get; set; }
        public CustomerVM Customer { get; set; }
        public EmployeeVM Employee { get; set; }
        public EventVM Event { get; set; }
        public double Amount { get; set; }
        public int VatPercentage { get; set; }
        public DateTime TimeSend { get; set; }
        public string content { get; set; }
    }
}
