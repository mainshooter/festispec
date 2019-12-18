using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.customer.pages
{
    public class AddCustomerVm : ViewModelBase
    {
        public CustomerOverviewVm CustomerList { get; set; }
        public CustomerVM Customer { get; set; }
        public ICommand AddCustomerCommand { get; set; }
        public ICommand CloseAddCustomerCommand { get; set; }

        public AddCustomerVm(CustomerOverviewVm customerList)
        {
            CustomerList = customerList;
            Customer = new CustomerVM();
            AddCustomerCommand = new RelayCommand(AddCustomer);
            CloseAddCustomerCommand = new RelayCommand(CloseAddCustomer);
        }

        private void AddCustomer()
        {
            using (var context = new Entities())
            {
                var customers = new List<Domain.Customer>(context.Customers);

                if (customers.Select(c => c.Email).Any(email => email == Customer.Email))
                {
                    var toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
                    toast.ShowError("Een gebeuiker met dit email adres bestaat al");
                    return;
                }
            }

            using (var context = new Entities())
            {
                context.Customers.Add(Customer.ToModel());
                context.SaveChanges();
            }

            CustomerList.CustomerList.Add(Customer);
            CustomerList.RaisePropertyChanged("EmployeeListFiltered");
            MessageBox.Show("Customer added", "Bla", MessageBoxButton.OK, MessageBoxImage.Hand);
            CloseAddCustomer();
        }

        private void CloseAddCustomer()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerPage) });
        }
    }
}
