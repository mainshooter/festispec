using Festispec.ViewModel.customer;
using Festispec.ViewModel.customer.pages;

namespace Festispec.Message
{
    class ChangeSelectedCustomerMessage
    {
        public CustomerVM Customer { get; set; }
        public CustomerOverviewVm CustomerList { get; set; }
    }
}
