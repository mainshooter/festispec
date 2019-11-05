using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.customer.contactPerson
{
    public class ContactPersonVM
    {
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Rol { get; set; }
    }
}
