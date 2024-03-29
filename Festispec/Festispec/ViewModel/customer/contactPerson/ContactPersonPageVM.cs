﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Customer.ContactPerson;
using Festispec.View.Pages.Customer.Note;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class ContactPersonPageVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filterItems;
        private ObservableCollection<ContactPersonVM> _filteredContactPersonList;

        public ICommand OpenContactPersonNotesCommand { get; set; }
        public ICommand DeleteContactPersonCommand { get; set; }
        public ICommand OpenAddContactPerson { get; set; }
        public ICommand EditContactPersonCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public string SelectedFilter { get; set; }
        public CustomerVM CustomerVM { get; set; }

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                RaisePropertyChanged(() => FilteredContactPersonList);
            }
        }

        public List<string> FilterItems
        {
            get => _filterItems;
            set
            {
                _filterItems = new List<string>();
                _filterItems.Add("Geen Filter");
                _filterItems.Add("Voornaam");
                _filterItems.Add("Achternaam");
                _filterItems.Add("Telefoonnummer");
                _filterItems.Add("E-mail");
            }
        }

        public ObservableCollection<ContactPersonVM> FilteredContactPersonList
        {
            get
            {
                var temp = new ObservableCollection<ContactPersonVM>();
                if(Filter != null)
                {
                    switch (SelectedFilter)
                    {
                        case "Geen Filter":
                            temp = _filteredContactPersonList;
                        break;
                        case "Voornaam":
                            temp = new ObservableCollection<ContactPersonVM>(_filteredContactPersonList.Select(cp => cp).Where(i => i.Firstname.ToLower().Contains(Filter.ToLower())));
                        break;
                        case "Achternaam":
                            temp = new ObservableCollection<ContactPersonVM>(_filteredContactPersonList.Select(cp => cp).Where(i => i.Lastname.ToLower().Contains(Filter.ToLower())));
                        break;
                        case "Telefoonnummer":
                            temp = new ObservableCollection<ContactPersonVM>(_filteredContactPersonList.Select(cp => cp).Where(i => i.Phone.ToLower().Contains(Filter.ToLower())));
                        break;
                        case "E-Mail":
                            temp = new ObservableCollection<ContactPersonVM>(_filteredContactPersonList.Select(cp => cp).Where(i => i.Email.ToLower().Contains(Filter.ToLower())));
                        break;
                    }
                }
                return temp;
            }
            set
            {
                _filteredContactPersonList = value;
                RaisePropertyChanged(() => FilteredContactPersonList);
            }
        }

        public ContactPersonVM SelectedContactPerson { get; set; }

        public ContactPersonPageVM()
        {
            MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            {
                CustomerVM = message.Customer;
                FillList();
                RaisePropertyChanged(() => CustomerVM);
            });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(ContactPersonPage))
                {
                    FillList();
                    RaisePropertyChanged(() => FilteredContactPersonList);
                }
            });

            using (var context = new Entities())
            {
                CustomerVM = new CustomerVM(context.Customers.First(c => c.Id == 1));
                RaisePropertyChanged(() => CustomerVM);
            }

            FillList();
            FilterItems = new List<string>();
            SelectedFilter = FilterItems.First();
            Filter = "";
            OpenContactPersonNotesCommand = new RelayCommand(OpenNotesPage);
            DeleteContactPersonCommand = new RelayCommand(DeleteContactPerson);
            OpenAddContactPerson = new RelayCommand(OpenAddContactPersonPage);
            EditContactPersonCommand = new RelayCommand(OpenEditContactPersonPage);
            BackCommand = new RelayCommand(Back);
        }

        public void Back() 
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerOverviewPage) });
        }

        public void FillList()
        {
            using(var context = new Entities())
            {
                FilteredContactPersonList = new ObservableCollection<ContactPersonVM>(context.ContactPersons.ToList()
                    .Select(cp => new ContactPersonVM(cp))
                    .Where(cp => cp.CustomerId == CustomerVM.Id));
            }
        }

        public void OpenNotesPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(NoteListPage)});
            MessengerInstance.Send<ChangeSelectedContactPersonMessage>(new ChangeSelectedContactPersonMessage()
            {
                ActualContactPerson = SelectedContactPerson
            });
        }

        public void OpenAddContactPersonPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddContactPersonPage) });
            MessengerInstance.Send<ChangeSelectedCustomerMessage>(new ChangeSelectedCustomerMessage()
            {
                Customer = CustomerVM
            });
        }

        public void OpenEditContactPersonPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditContactPersonPage) });
            MessengerInstance.Send<ChangeSelectedContactPersonMessage>(new ChangeSelectedContactPersonMessage()
            {
                ActualContactPerson = SelectedContactPerson
            }) ;
        }

        public void DeleteContactPerson()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze contactpersoon wilt verwijderen?", "Contactpersoon Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes) && CanDeleteContactPerson())
            {
                DeleteContactPersonNotes();
                SelectedContactPerson.Notes.Clear();
                using(var context = new Entities())
                {
                    context.ContactPersons.Remove(context.ContactPersons.Where(p => p.Id == SelectedContactPerson.Id).First());
                    context.SaveChanges();
                }
                FillList();
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Contactpersoon verwijderd");
                RaisePropertyChanged(() => FilteredContactPersonList);
            }
        }

        private void DeleteContactPersonNotes()
        {
            using (var context = new Entities())
            {
                var ContactPersonNotes = context.Notes.Where(cp => cp.ContactPersonId == SelectedContactPerson.Id);
                foreach (var note in ContactPersonNotes)
                {
                    context.Notes.Remove(note);
                }
                context.SaveChanges();
            }
        }

        private bool CanDeleteContactPerson()
        {
            using(var context = new Entities())
            {
                if (context.Events.Where(e => e.ContactPersonId == SelectedContactPerson.Id).Any())
                {
                    MessageBox.Show("Contactpersoon kan niet verwijderd worden omdat deze gekoppeld is aan een of meerdere evenementen.", "Waarschuwing", MessageBoxButton.OK);
                    return false;
                }
                return true;
            }
        }
    }
}
