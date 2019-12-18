using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class EditCustomerVm : ViewModelBase
    {
        public CustomerOverviewVm CustomerList { get; set; }
        public CustomerVM Customer { get; set; }
        public ICommand EditCustomerCommand { get; set; }
        public ICommand CloseEditCustomerCommand { get; set; }

        public EditCustomerVm(CustomerOverviewVm customerList)
        {
            CustomerList = customerList;
            Customer = new CustomerVM();
            EditCustomerCommand = new RelayCommand(EditCustomer);
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
                    toast.ShowError("Een gebeuiker met dit email adres bestaat al");
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
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerPage) });
        }
    }
}
