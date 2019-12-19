using System.Linq;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Note;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel.customer.contactPerson.note
{
    public class NoteListVM : ViewModelBase
    {
        public ContactPersonVM ContactPerson { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand OpenAddNoteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public NoteListVM()
        {
            // Dit hieronder terugzetten wanneer de message er is
//            MessengerInstance.Register<ChangeSelectedContactPersonMessage>(this, message =>
//            {
//                ContactPerson = message.ContactPerson;
//            });

            // Dit weghalen wanneer de message er is
            using (var context = new Entities())
            {
                ContactPerson = new ContactPersonVM(context.ContactPersons.First(cp => cp.Id == 1));
            }

            BackCommand = new RelayCommand(Back);
            OpenAddNoteCommand = new RelayCommand(OpenAddNote);
            SaveCommand = new RelayCommand<NoteVM>(SaveNote);
            DeleteCommand = new RelayCommand<NoteVM>(DeleteNote);
        }

        private void Back()
        {
            // Lijn hieronder terugzetten wanneer de message er is
            //MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ContactPersonPage) });
        }

        private void OpenAddNote()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddNotePage) });
        }

        private void SaveNote(NoteVM source)
        {
            if (!CanSave(source)) return;

            using (var context = new Entities())
            {
                context.Notes.Attach(source.ToModel());
                context.Entry(source.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Opgeslagen");
            }
        }

        private bool CanSave(NoteVM source)
        {
            if (source.Note == null || source.Note.Length > 10000 || source.Note == "")
            {
                MessageBox.Show("De notitie mag niet leeg zijn of meer dan 10.000 karakters hebben.", "Waarschuwing", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        private void DeleteNote(NoteVM source)
        {
            if (MessageBox.Show("Weet je zeker dat je deze notitie wil verwijderen?", "Weet je het zeker?", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            using (var context = new Entities())
            {
                context.Notes.Attach(source.ToModel());
                context.Notes.Remove(source.ToModel());
                context.SaveChanges();
                ContactPerson.Notes.Remove(source);
            }
        }
    }
}
