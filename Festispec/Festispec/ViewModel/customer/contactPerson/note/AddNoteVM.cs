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

            MessengerInstance.Register<ChangeSelectedContactPersonMessage>(this, message => { _contactPerson = message.ActualContactPerson; });

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
                Note.ContactPersonId = _contactPerson.Id;
                Note.Time = DateTime.Now;
                context.Notes.Add(Note.ToModel());
                context.SaveChanges();
                _contactPerson.Notes.Add(Note);
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
