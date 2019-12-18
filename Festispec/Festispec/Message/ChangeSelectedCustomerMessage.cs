using Festispec.ViewModel.customer;

namespace Festispec.Message
{
    class ChangeSelectedCustomerMessage
    {
        public CustomerVM Customer { get; set; }
        public CustomerOverviewVm CustomerOverview { get; set; }
    }
}
