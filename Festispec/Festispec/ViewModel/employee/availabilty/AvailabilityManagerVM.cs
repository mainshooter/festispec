using Festispec.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
                RaisePropertyChanged("WeekNumber");
            }
        }

        public int WeekNumber
        {
            get
            {
                return SelectedWeek.Week.DayOfYear / 7;
            }
        }

        public int Year
        {
            get
            {
                return SelectedWeek.Week.Year;
            }
        }

        public ICommand NextWeekCommand { get; set; }
        public ICommand PreviousWeekCommand { get; set; }

        public AvailabilityManagerVM()
        {
            SelectedWeek = new AvailabilityWeekVM(CurrentWeek());
            Weeks = new ObservableCollection<AvailabilityWeekVM>();
            Weeks.Add(SelectedWeek);
            NextWeekCommand = new RelayCommand(NextWeek);
            PreviousWeekCommand = new RelayCommand(PreviousWeek, CanSwitchWeek);
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
                    context.Entry(week.Monday).State = EntityState.Modified;
                    context.Entry(week.Tuesday).State = EntityState.Modified;
                    context.Entry(week.Wednesday).State = EntityState.Modified;
                    context.Entry(week.Thursday).State = EntityState.Modified;
                    context.Entry(week.Friday).State = EntityState.Modified;
                    context.Entry(week.Saturday).State = EntityState.Modified;
                    context.Entry(week.Sunday).State = EntityState.Modified;
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
    }
}
