using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Note;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class ContactPersonPageVM : ViewModelBase
    {
        private string filter;
        private List<string> filterItems;
        private ObservableCollection<ContactPersonVM> _filteredContactPersonList;
        private CustomerVM customerVM;
        private ContactPersonVM selectedContactPerson;

        public ICommand OpenContactPersonNotes { get; set; }
        public string CustomerName => CustomerVM?.Name;
        public string SelectedFilter { get; set; }

        public CustomerVM CustomerVM 
        {
            get => customerVM;
            set
            {
                customerVM = value;
                RaisePropertyChanged(() => CustomerName);
            }
        }

        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                RaisePropertyChanged(() => FilteredContactPersonList);
            }
        }

        public List<string> FilterItems
        {
            get => filterItems;
            set
            {
                filterItems = new List<string>();
                filterItems.Add("Geen Filter");
                filterItems.Add("Voornaam");
                filterItems.Add("Achternaam");
                filterItems.Add("Telefoonnummer");
                filterItems.Add("E-mail");
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

        public ContactPersonVM SelectedContactPerson
        {
            get => selectedContactPerson;
            set
            {
                if(value != null)
                {
                    selectedContactPerson = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ContactPersonPageVM()
        {
            //Als er message met customer is deze weghalen
            //MessengerInstance.Register<ChangeSelectedCustomerMessage>(this, message =>
            //{
            //    customerVM = message.Customer;
            //});

            using(var context = new Entities())
            {
                CustomerVM = new CustomerVM(context.Customers.First(c => c.Id == 1));
            }

            FillList();

            FilterItems = new List<string>();
            SelectedFilter = FilterItems.First();
            Filter = "";

            OpenContactPersonNotes = new RelayCommand(OpenNotesPage);
        }

        public void FillList()
        {
            using(var context = new Entities())
            {
                _filteredContactPersonList = new ObservableCollection<ContactPersonVM>(context.ContactPersons.ToList()
                    .Select(cp => new ContactPersonVM(cp))
                    .Where(cp => cp.CustomerId == customerVM.Id));
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
    }
}
