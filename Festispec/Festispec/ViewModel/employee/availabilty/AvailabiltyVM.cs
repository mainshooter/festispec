using Festispec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.availabilty
{
    public class AvailabiltyVM : ViewModelBase
    {
        private AvailabilityInspector _availabilityInspector;
        private EmployeeVM _employee;
        private DateTime? _availabilityStart;
        private DateTime? _availabilityEnd;

        public int Id
        {
            get
            {
                return _availabilityInspector.Id;
            }
            private set
            {
                _availabilityInspector.Id = value;
            }
        }

        public EmployeeVM Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                // _availabilityInspector.Employee = value.ToModel();
                _availabilityInspector.EmployeeId = value.Id;
            }
        }

        public DateTime? AvailabiltyStart
        {
            get
            {
                if (_availabilityStart == null)
                {
                    return _availabilityStart;
                }
                return _availabilityInspector.AvailableFrom;
            }
            set
            {
                if (value != null)
                {
                    _availabilityInspector.AvailableFrom = (DateTime)value;
                }
                _availabilityStart = value;
                if (AvailabiltyStart > AvailabiltyEnd)
                {
                    AvailabiltyEnd = AvailabiltyStart;
                }
                RaisePropertyChanged("AvailabiltyStart");
                RaisePropertyChanged("AvailabiltyEnd");
            }
        }

        public DateTime? AvailabiltyEnd
        {
            get
            {
                if (_availabilityEnd == null)
                {
                    return _availabilityStart;
                }
                return _availabilityInspector.AvailableTill;
            }
            set
            {
                if (value != null)
                {
                    _availabilityInspector.AvailableTill = (DateTime)value;
                }
                _availabilityEnd = value;
                if (AvailabiltyEnd < AvailabiltyStart)
                {
                    AvailabiltyEnd = AvailabiltyStart;
                }
                RaisePropertyChanged("AvailabiltyEnd");
                RaisePropertyChanged("AvailabiltyStart");
            }
        }

        public AvailabiltyVM(AvailabilityInspector av)
        {
            _availabilityInspector = av;
            Employee = new EmployeeVM(av.Employee);
        }

        public AvailabiltyVM()
        {
            _availabilityInspector = new AvailabilityInspector();
            using (var context = new Entities())
            {
                Employee = new EmployeeVM(context.Employees.Where(employee => employee.Id == 2).FirstOrDefault());
            }
        }

        public AvailabilityInspector ToModel()
        {
            return _availabilityInspector;
        }
    }
}
