using Festispec.ViewModel.customer.contactPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EventVM
    {
        public int Id { get; set; }
        public CustomerVM Customer { get; set; }
        public ContactPersonVM ContactPerson { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfVisitors { get; set; }
        public int SurveysArea { get; set; }
        public string Description { get; set; }
    }
}
