using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.customerEvent;

namespace Festispec.Message
{
    public class ChangeSelectedEventMessage
    {
        public EventVM Event { get; set; }
        public EventListVM EventList { get; set; }
    }
}
