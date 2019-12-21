using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.ContactPerson;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class EditContactPersonVM : ViewModelBase
    {
        private ContactPersonVM contactPerson { get; set; }

        public ICommand BackCommand { get; set; }
        public ICommand EditContactPersonCommand { get; set; }

        public ContactPersonVM ContactPerson
        {
            get
            {
                return contactPerson;
            }
            set
            {
                contactPerson = value;
                RaisePropertyChanged();
            }
        }

        public EditContactPersonVM()
        {
            this.MessengerInstance.Register<ChangeSelectedContactPersonMessage>(this, message =>
            {
                ContactPerson = message.ActualContactPerson;
                RaisePropertyChanged();
            });

            BackCommand = new RelayCommand(GoBackButton);
            EditContactPersonCommand = new RelayCommand(EditContactPerson);
        }

        private void GoBackButton()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ContactPersonPage) });
        }

        private void EditContactPerson()
        {
            if (ContactPerson.IsValid)
            {
                using(var context = new Entities())
                {
                    ContactPerson temp = ContactPerson.ToModel();
                    context.Entry(temp).State = EntityState.Modified;
                    context.SaveChanges();
                }
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Aangepast!");
                GoBackButton();
            }
            else
            {
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowError("Kijk of alles goed is ingevuld!");
            }
        }
    }
}
