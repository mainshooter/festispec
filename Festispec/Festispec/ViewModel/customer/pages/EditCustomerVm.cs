using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.customer.pages
{
    public class EditCustomerVm : ViewModelBase
    {
        private CustomerVM _customer;
        public CustomerOverviewVm CustomerList { get; set; }
        public CustomerVM Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                RaisePropertyChanged("Customer");
            }
        }
        public ICommand EditCustomerCommand { get; set; }
        public ICommand CloseEditCustomerCommand { get; set; }

        public EditCustomerVm(CustomerOverviewVm customerList)
        {
            MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            {
                CustomerList = message.CustomerList;
                Customer = message.Customer;
            });

            CustomerList = customerList;
            EditCustomerCommand = new RelayCommand(EditCustomer, CanAddCustomer);
            CloseEditCustomerCommand = new RelayCommand(CloseEditCustomer);
        }

        private void EditCustomer()
        {
            using (var context = new Entities())
            {
                var customers = new List<Domain.Customer>(context.Customers);
                if (customers.Select(employee => employee).Any(employee => employee.Email == Customer.Email && employee.Id != Customer.Id))
                {
                    var toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
                    toast.ShowError("Een gebruiker met dit email adres bestaat al");
                    return;
                }
            }

            using (var context = new Entities())
            {
                context.Entry(Customer.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }

            CloseEditCustomer();
        }

        private void CloseEditCustomer()
        {
            MessengerInstance.Send(new ChangePageMessage() { NextPageType = typeof(CustomerPage) });
        }

        private bool CanAddCustomer()
        {
            return Customer != null && Customer.IsValid;
        }
    }
}
