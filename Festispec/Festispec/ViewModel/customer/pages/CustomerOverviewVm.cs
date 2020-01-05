﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Customer.ContactPerson;

namespace Festispec.ViewModel.customer.pages
{
    public class CustomerOverviewVm : ViewModelBase
    {
        private string _filter;
        private List<string> _filters;
        private CustomerVM _selectedCustomer;

        public ICommand OpenAddCustomer { get; set; }
        public ICommand OpenEditCustomer { get; set; }
        public ICommand OpenSingleCustomer { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }
        public ICommand OpenCustomerEventCommand { get; set; }
        public ICommand OpenContactPersonCommand { get; set; }
        public ObservableCollection<CustomerVM> CustomerList { get; set; }
        public string SelectedFilter { get; set; }
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                RaisePropertyChanged("CustomerListFiltered");
            }
        }
        public List<string> Filters
        {
            get => _filters;
            set =>
                _filters = new List<string>
                {
                    "Naam",
                    "Plaats",
                    "E-mail"
                };
        }
        public ObservableCollection<CustomerVM> CustomerListFiltered
        {
            get
            {
                if (Filter != null && !Filter.Equals(""))
                {
                    switch (SelectedFilter)
                    {
                        case "Naam":
                            return new ObservableCollection<CustomerVM>(CustomerList.Select(customer => customer).Where(customer => customer.Name.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Plaats":
                            return new ObservableCollection<CustomerVM>(CustomerList.Select(customer => customer).Where(customer => customer.City.ToLower().Contains(Filter.ToLower())).ToList());
                        case "E-mail":
                            return new ObservableCollection<CustomerVM>(CustomerList.Select(customer => customer).Where(customer => customer.Email.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Website":
                            return new ObservableCollection<CustomerVM>(CustomerList.Select(customer => customer).Where(customer => customer.Website.ToLower().Contains(Filter.ToLower())).ToList());
                    }
                }

                return CustomerList;
            }
        }
        public CustomerVM SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (value != null)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged("SelectedCustomer");
                }
            }
        }

        public CustomerOverviewVm()
        {
            var customerRepository = new CustomerRepository();

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(CustomerOverviewPage))
                {
                    RefreshCustomers();
                }
            });

            Filters = new List<string>();
            SelectedFilter = Filters.First();
            Filter = "";
            CustomerList = new ObservableCollection<CustomerVM>(customerRepository.GetCustomers());

            OpenAddCustomer = new RelayCommand(OpenAddCustomerPage);
            OpenEditCustomer = new RelayCommand(OpenEditCustomerPage);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer);
            OpenSingleCustomer = new RelayCommand(OpenCustomerDetailsPage);
            OpenCustomerEventCommand = new RelayCommand(OpenEventsPage);
            OpenContactPersonCommand = new RelayCommand(OpenContactPerson);
        }

        public void OpenContactPerson()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ContactPersonPage) });
            MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage() { Customer = SelectedCustomer });
        }

        private void OpenAddCustomerPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddCustomerPage) });
            MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage()
            {
                Customer = new CustomerVM(),
                CustomerList = this
            });
        }

        private void OpenEditCustomerPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditCustomerPage) });
            MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage()
            {
                Customer = SelectedCustomer,
                CustomerList = this
            });
        }

        private void OpenCustomerDetailsPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerDetailsPage) });
            MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage { Customer = SelectedCustomer, CustomerList = this});
        }

        private void OpenEventsPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
            MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage { Customer = SelectedCustomer, CustomerList = this });
        }

        private void DeleteCustomer()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze klant wilt verwijderen?", "Klant Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    var temp = SelectedCustomer.ToModel();
                    var orders = context.Orders.Where(o => o.CustomerId == temp.Id);

                    if (orders.Any())
                    {
                        MessageBox.Show("Klant kan niet worden verwijderd omdat hij opdrachten heeft", "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        context.Customers.Remove(context.Customers.Select(customer => customer).First(customer => customer.Id == temp.Id));
                        context.SaveChanges();
                        MessageBox.Show("Klant verwijderd", "Gelukt", MessageBoxButton.OK);
                        CustomerList.Remove(SelectedCustomer);
                        RaisePropertyChanged("CustomerListFiltered");
                    }
                }
            }
        }

        public void RefreshCustomers()
        {
            var customerRepository = new CustomerRepository();
            CustomerList = new ObservableCollection<CustomerVM>(customerRepository.GetCustomers());
            RaisePropertyChanged("CustomerListFiltered");
        }
    }
}
