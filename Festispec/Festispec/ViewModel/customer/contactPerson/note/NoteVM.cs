using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.customer.contactPerson.note
{
    public class NoteVM
    {
        public int Id { 
            get {
                return _note.Id;
            }
            private set {
                _note.Id = value;
            }
        }
        private ContactPersonVM _contactPerson;
        public ContactPersonVM ContactPerson {
            get {
                return _contactPerson;
            }
            set {
                _contactPerson = value;
            }
        }
        public string Note { 
            get {
                return _note.Note1;
            }
            set {
                _note.Note1 = value;
            }
        }
        public DateTime Time {
            get {
                return _note.Time;
            }
            set {
                _note.Time = value;
            }
        }
        private Note _note;
        public NoteVM(Note note)
        {
            _note = note;
        }
        public NoteVM()
        {
            _note = new Note();
        }
    }
}
