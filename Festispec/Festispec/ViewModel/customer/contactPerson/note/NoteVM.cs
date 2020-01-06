using Festispec.Domain;
using System;

namespace Festispec.ViewModel.customer.contactPerson.note
{
    public class NoteVM
    {
        private Note _note;

        public int Id => _note.Id;

        public int ContactPersonId
        {
            get => _note.ContactPersonId;
            set => _note.ContactPersonId = value;
        }

        public string Note
        { 
            get => _note.Note1;
            set => _note.Note1 = value;
        }

        public DateTime Time
        {
            get => _note.Time;
            set => _note.Time = value;
        }

        public string TimeString => Time.ToString("dd-MM-yyyy HH:mm");

        public NoteVM(Note note)
        {
            _note = note;
        }

        public NoteVM()
        {
            _note = new Note();
        }

        public Note ToModel()
        {
            return _note;
        }
    }
}
