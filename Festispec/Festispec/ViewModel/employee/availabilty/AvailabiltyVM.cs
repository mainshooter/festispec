using Festispec.Domain;
using Festispec.ViewModel.auth;
using GalaSoft.MvvmLight;
using System;

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
                RaisePropertyChanged("MaxEndTime");
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
                RaisePropertyChanged("MaxEndTime");
                if (value != null)
                {
                    if (value >= MaxEndTime)
                    {
                        value = MaxEndTime;
                    }
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

        public DateTime? MaxEndTime
        {
            get
            {
                if (AvailabiltyStart != null)
                {
                    if (AvailabiltyStart.Value.Year > 1)
                    {
                        var time = new DateTime();
                        time = time.AddYears(AvailabiltyStart.Value.Year - 1); ;
                        time = time.AddMonths(AvailabiltyStart.Value.Month - 1);
                        time = time.AddDays(AvailabiltyStart.Value.Day - 1);
                        time = time.AddHours(23);
                        time = time.AddMinutes(59);
                        return time;
                    }
                }
                return null;
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
