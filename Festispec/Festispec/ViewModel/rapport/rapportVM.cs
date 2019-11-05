using Festispec.ViewModel.employee.assignment;
using Festispec.ViewModel.rapport.element;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.rapport
{
    public class rapportVM
    {
        public int Id { get; set; }
        public AssignmentVM Assignment { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public ObservableCollection<RapportElementVM> RapportElements { get; set; }
    }
}
