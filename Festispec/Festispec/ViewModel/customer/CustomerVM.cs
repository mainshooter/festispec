using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.customer
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int COC { get; set; }
        public int EstablishmentNumber { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Website { get; set; }
    }
}
