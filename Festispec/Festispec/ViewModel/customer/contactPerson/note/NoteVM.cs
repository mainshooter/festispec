using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.customer.contactPerson.note
{
    public class NoteVM
    {
        public int Id { get; set; }
        public ContactPersonVM ContactPerson { get; set; }
        public string Note { get; set; }
        public DateTime Time { get; set; }
    }
}
