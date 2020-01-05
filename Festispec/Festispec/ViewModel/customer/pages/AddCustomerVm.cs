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
        private CustomerVM _customer;
        public CustomerOverviewVm CustomerList { get; set; }

        public CustomerVM Customer 
        { 
            get => _customer;
            set 
            {
                _customer = value;
                RaisePropertyChanged(() => Customer);
            }
        }

        public ICommand AddCustomerCommand { get; set; }
        public ICommand CloseAddCustomerCommand { get; set; }

        public AddCustomerVm(CustomerOverviewVm customerList)
        {
            MessengerInstance.Register<ChangePageMessage>(this, message => { 
                if (message.NextPageType == typeof(AddCustomerPage))
                {
                    Customer = new CustomerVM();
                }
            });

            Customer = new CustomerVM();
            CustomerList = customerList;
            AddCustomerCommand = new RelayCommand(AddCustomer, CanAddCustomer);
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
                    toast.ShowError("Een gebruiker met dit email adres bestaat al");
                    return;
                }
            }

            using (var context = new Entities())
            {
                var contactPersonModel = Customer.ToModel();
                context.Customers.Add(contactPersonModel);
                context.SaveChanges();
            }

            CustomerList.CustomerList.Add(Customer);
            CustomerList.RaisePropertyChanged("EmployeeListFiltered");
            MessageBox.Show("Klant toegevoegd", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseAddCustomer();
        }

        private void CloseAddCustomer()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerOverviewPage) });
        }

        private bool CanAddCustomer()
        {
            return Customer != null && Customer.IsValid;
        }
    }
}
