﻿using Festispec.Domain;
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
                RaisePropertyChanged("SelectedWeek");
                RaisePropertyChanged("WeekNumber");
                RaisePropertyChanged("Year");
            }
        }

        public int WeekNumber
        {
            get
            {
                return GetIso8601WeekOfYear(SelectedWeek.Week);
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
                    ModifyAvailability(context, week.Monday, 1);
                    ModifyAvailability(context, week.Tuesday, 2);
                    ModifyAvailability(context, week.Wednesday, 3);
                    ModifyAvailability(context, week.Thursday, 4);
                    ModifyAvailability(context, week.Friday, 5);
                    ModifyAvailability(context, week.Saturday, 6);
                    ModifyAvailability(context, week.Sunday, 0);
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

        private void ModifyAvailability(Entities context, AvailabiltyVM availabiltyVM, int dayOfWeek)
        {
            if (availabiltyVM.AvailabiltyStart != null && availabiltyVM.AvailabiltyEnd != null)
            {
                if (context.AvailabilityInspectors.Select(availability => availability).Where(availability => availability.Id == availabiltyVM.Id).FirstOrDefault() == null)
                {
                    SetDate(availabiltyVM, dayOfWeek);
                    context.Set<AvailabilityInspector>().AddOrUpdate(availabiltyVM.ToModel());
                }
                else
                {
                    context.Set<AvailabilityInspector>().AddOrUpdate(availabiltyVM.ToModel());
                }
            }
            else if (context.AvailabilityInspectors.Select(availability => availability).Where(availability => availability.Id == availabiltyVM.Id).FirstOrDefault() != null)
            {
                context.AvailabilityInspectors.Remove(context.AvailabilityInspectors.Where(a => a.Id == availabiltyVM.Id).FirstOrDefault());
            }
        }

        private void SetDate(AvailabiltyVM availabilty, int dayOfWeek)
        {
            if (dayOfWeek == 0)
            {
                dayOfWeek = 7;
            }

            var daysUntilDay = (int)(dayOfWeek - SelectedWeek.Week.DayOfWeek);
            var SelectedDay = SelectedWeek.Week;
            var startTimeHour = availabilty.AvailabiltyStart.Value.Hour;
            var startTimeMinute = availabilty.AvailabiltyStart.Value.Minute;
            var endTimeHour = availabilty.AvailabiltyEnd.Value.Hour;
            var endTimeMinute = availabilty.AvailabiltyEnd.Value.Minute;
            SelectedDay = SelectedDay.AddDays(daysUntilDay);

            availabilty.AvailabiltyStart = SelectedDay;
            availabilty.AvailabiltyEnd = SelectedDay;
            availabilty.AvailabiltyStart = availabilty.AvailabiltyStart.Value.AddHours(startTimeHour);
            availabilty.AvailabiltyStart = availabilty.AvailabiltyStart.Value.AddMinutes(startTimeMinute);
            availabilty.AvailabiltyEnd = availabilty.AvailabiltyEnd.Value.AddHours(endTimeHour);
            availabilty.AvailabiltyEnd = availabilty.AvailabiltyEnd.Value.AddMinutes(endTimeMinute);
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
