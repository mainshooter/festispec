using Festispec.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.employee.availabilty
{
    public class AvailabilityManagerVM : ViewModelBase
    {
        private AvailabilityWeekVM _selectedWeek;

        public ObservableCollection<AvailabilityWeekVM> Weeks { get; set; }
        public AvailabilityWeekVM SelectedWeek
        {
            get
            {
                return _selectedWeek;
            }
            set
            {
                _selectedWeek = value;
                RaisePropertyChanged();
            }
        }

        public int WeekNumber
        {
            get
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                var week = cal.GetWeekOfYear(SelectedWeek.Week, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                if (week == 53)
                {
                    week = 1;
                }
                return week;
            }
        }

        public int Year
        {
            get
            {
                var year = SelectedWeek.Week;
                return year.AddDays(7).Year;
            }
        }

        public ICommand NextWeekCommand { get; set; }
        public ICommand PreviousWeekCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        public AvailabilityManagerVM()
        {
            SelectedWeek = new AvailabilityWeekVM(CurrentWeek());
            Weeks = new ObservableCollection<AvailabilityWeekVM>();
            Weeks.Add(SelectedWeek);
            NextWeekCommand = new RelayCommand(NextWeek);
            PreviousWeekCommand = new RelayCommand(PreviousWeek, CanSwitchWeek);
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        public void NextWeek()
        {
            var temp = SelectedWeek;
            if (SelectedWeek.NextWeek == null)
            {
                SelectedWeek.NextWeek = new AvailabilityWeekVM(SelectedWeek.Week.AddDays(7));
                Weeks.Add(SelectedWeek.NextWeek);
            }
            SelectedWeek = SelectedWeek.NextWeek;
            SelectedWeek.PreviousWeek = temp;
        }

        public void PreviousWeek()
        {
            SelectedWeek = SelectedWeek.PreviousWeek;
        }

        public bool CanSwitchWeek()
        {
            if (SelectedWeek.PreviousWeek == null)
            {
                return false;
            }
            return true;
        }

        public void SaveChanges()
        {
            using (var context = new Entities())
            {
                foreach (var week in Weeks)
                {
                    ModifyAvailability(context, week.Monday);
                    ModifyAvailability(context, week.Tuesday);
                    ModifyAvailability(context, week.Wednesday);
                    ModifyAvailability(context, week.Thursday);
                    ModifyAvailability(context, week.Friday);
                    ModifyAvailability(context, week.Saturday);
                    ModifyAvailability(context, week.Sunday);
                }
                context.SaveChanges();
            }
        }

        public bool CanSaveChanges()
        {
            return true;
        }

        public DateTime CurrentWeek()
        {
            int diff = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
            return DateTime.Today.AddDays(-1 * diff).Date;
        }

        private void ModifyAvailability(Entities context, AvailabiltyVM availabiltyVM)
        {
            if (availabiltyVM.AvailabiltyStart != null && availabiltyVM.AvailabiltyEnd != null)
            {
                if (context.AvailabilityInspectors.Select(availability => availability).Where(availability => availability.Id == availabiltyVM.Id).FirstOrDefault() == null)
                {
                    context.AvailabilityInspectors.Add(availabiltyVM.ToModel());
                }
                else
                {
                    context.Set<AvailabilityInspector>().AddOrUpdate(availabiltyVM.ToModel());
                }
            }
            else if (context.AvailabilityInspectors.Select(availability => availability).Where(availability => availability.Id == availabiltyVM.Id).FirstOrDefault() != null)
            {
                context.AvailabilityInspectors.Remove(availabiltyVM.ToModel());
                context.Entry(availabiltyVM.ToModel()).State = EntityState.Deleted;
            }
        }
    }
}
