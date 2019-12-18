using System;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Note;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.customer.contactPerson.note
{
    public class AddNoteVM : ViewModelBase
    {
        private ContactPersonVM _contactPerson;

        public NoteVM Note { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public AddNoteVM()
        {
            Note = new NoteVM();

            // Lijn hieronder terugzetten wanneer de message er is
            //MessengerInstance.Register<ChangeSelectedContactPersonMessage>(this, message => { _contactPerson = message.ContactPerson; });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddNotePage))
                {
                    Note = new NoteVM();
                    RaisePropertyChanged(() => Note);
                }
            });

            BackCommand = new RelayCommand(Back);
            SaveCommand = new RelayCommand(Save);
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(NoteListPage) });
        }

        private void Save()
        {
            if (!CanSave()) return;

            using (var context = new Entities())
            {
                // Lijn hieronder terugzetten wanneer de message er is
                //Note.ContactPersonId = _contactPerson.Id;
                // Lijn hieronder gehalen wanneer de message er is
                Note.ContactPersonId = 1;
                Note.Time = DateTime.Now;
                context.Notes.Add(Note.ToModel());
                context.SaveChanges();
                // Lijn hieronder terugzetten wanneer de message er is
                //_contactPerson.Notes.Add(Note);
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Opgeslagen");
                Back();
            }
        }

        private bool CanSave()
        {
            if (Note.Note == null || Note.Note.Length > 10000 || Note.Note == "")
            {
                MessageBox.Show("De notitie mag niet leeg zijn of meer dan 10.000 karakters hebben.", "Waarschuwing", MessageBoxButton.OK);
                return false;
            }

            return true;
        }
    }
}
