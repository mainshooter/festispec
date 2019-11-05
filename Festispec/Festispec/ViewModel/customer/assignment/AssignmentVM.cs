using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.assignment
{
    public class AssignmentVM
    {
        public int Id { get; set; }
        public CustomerVM Customer { get; set; }
        public EventVM Event { get; set; }
        public EmployeeVM Employee { get; set; }
        public OfferVM Offer { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
