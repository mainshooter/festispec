using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.availabilty
{
    public class AvailabiltyVM
    {
        private AvailabilityInspector _availabilityInspector;

        public AvailabiltyVM(AvailabilityInspector av)
        {
            _availabilityInspector = av;
            Employee = new EmployeeVM(av.Employee);
        }

        public AvailabiltyVM()
        {
            _availabilityInspector = new AvailabilityInspector();
        }

        public int Id {
            get {
                return _availabilityInspector.Id;
            }
            private set {
                _availabilityInspector.Id = value;
            }
        }

        public EmployeeVM Employee { get; set; }

        public DateTime AvailabiltyStart {
            get {
                return _availabilityInspector.AvailableFrom;
            }
            set {
                _availabilityInspector.AvailableFrom = value;
            }
        }

        public DateTime AvailabiltyEnd { 
            get {
                return _availabilityInspector.AvailableTill;
            }
            set {
                _availabilityInspector.AvailableTill = value;
            }
        }
    }
}
