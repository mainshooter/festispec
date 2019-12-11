using Festispec.ViewModel.Customer.order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Message
{
    public class ChangeSelectedOrderMessage
    {
        public OrderVM SelectedOrderVM { get; set; }
    }
}
