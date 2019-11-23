using Festispec.Domain;
using Festispec.ViewModel.auth;
using GalaSoft.MvvmLight;
using System;
using System.Linq;

namespace Festispec.ViewModel.employee.availabilty
{
    public class AvailabiltyVM : ViewModelBase
    {
        private AvailabilityInspector _availabilityInspector;
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
        }

        public AvailabiltyVM()
        {
            _availabilityInspector = new AvailabilityInspector();
            _availabilityInspector.EmployeeId = UserSessionVm.Current.Employee.Id;
        }

        public AvailabilityInspector ToModel()
        {
            return _availabilityInspector;
        }
    }
}
