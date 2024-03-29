﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.ContactPerson;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class AddContactPersonVM : ViewModelBase
    {
        public ContactPersonVM ContactPerson { get; set; }
        public CustomerVM Customer { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddContactPerson { get; set; }

        public AddContactPersonVM()
        {
            ContactPerson = new ContactPersonVM();
            this.MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            {
                Customer = message.Customer;
                RaisePropertyChanged(() => ContactPerson);
            });

            BackCommand = new RelayCommand(GoBackButton);
            AddContactPerson = new RelayCommand(AddContactPersonMethod);
        }

        private void AddContactPersonMethod()
        {
            if (ContactPerson.IsValid)
            {
                using (var context = new Entities())
                {
                    ContactPerson.CustomerId = Customer.Id;
                    context.ContactPersons.Add(ContactPerson.ToModel());
                    context.SaveChanges();
                }
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Contactperson aangemaakt");
                ContactPerson = new ContactPersonVM();
                GoBackButton();
            }
            else
            {
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowError("Kijk even of je alle velden hebt ingevuld!");
            }
        }

        private void GoBackButton()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ContactPersonPage) });
        }
    }
}
