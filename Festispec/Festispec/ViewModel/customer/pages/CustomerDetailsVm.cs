using System.Windows.Input;
using Festispec.Message;
using Festispec.View.Pages.Customer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel.customer.pages
{
    public class CustomerDetailsVm : ViewModelBase
    {
        private CustomerVM _customer;
        public CustomerOverviewVm CustomerList { get; set; }
        public CustomerVM Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                RaisePropertyChanged();
            }
        }
        public ICommand CloseCustomerOverviewCommand { get; set; }

        public CustomerDetailsVm(CustomerOverviewVm customerList)
        {
            MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            {
                CustomerList = message.CustomerList;
                Customer = message.Customer;
            });

            CustomerList = customerList;
            CloseCustomerOverviewCommand = new RelayCommand(CloseEditCustomer);
        }

        private void CloseEditCustomer()
        {
            MessengerInstance.Send(new ChangePageMessage() { NextPageType = typeof(CustomerPage) });
        }
    }
}
