using Festispec.Domain;
using Festispec.ViewModel.auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Festispec.ViewModel.employee.availabilty
{
    public class AvailabilityWeekVM
    {
        private List<AvailabiltyVM> _availabilities;
        public DateTime Week { get; set; }
        public AvailabilityWeekVM NextWeek { get; set; }
        public AvailabilityWeekVM PreviousWeek { get; set; }
        public AvailabiltyVM Monday { get; set; }
        public AvailabiltyVM Tuesday { get; set; }
        public AvailabiltyVM Wednesday { get; set; }
        public AvailabiltyVM Thursday { get; set; }
        public AvailabiltyVM Friday { get; set; }
        public AvailabiltyVM Saturday { get; set; }
        public AvailabiltyVM Sunday { get; set; }

        public AvailabilityWeekVM(DateTime week)
        {
            _availabilities = new List<AvailabiltyVM>();
            Week = week;
            bool hasMonday = false;
            bool hasTuesday = false;
            bool hasWednesday = false;
            bool hasThursday = false;
            bool hasFriday = false;
            bool hasSaturday = false;
            bool hasSunday = false;
            using (var context = new Entities())
            {
                var availabilities = context.AvailabilityInspectors.Select(availabilty => availabilty).Where(availabilty => availabilty.Employee.Id == UserSessionVm.Current.Employee.Id);
                foreach (var availability in availabilities)
                {
                    if (DatesAreInTheSameWeek(week, availability.AvailableFrom))
                    {
                        switch (availability.AvailableFrom.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                Monday = new AvailabiltyVM(availability);
                                Monday.AvailabiltyStart = availability.AvailableFrom;
                                Monday.AvailabiltyEnd = availability.AvailableTill;
                                hasMonday = true;
                                _availabilities.Add(Monday);
                                break;
                            case DayOfWeek.Tuesday:
                                Tuesday = new AvailabiltyVM(availability);
                                Tuesday.AvailabiltyStart = availability.AvailableFrom;
                                Tuesday.AvailabiltyEnd = availability.AvailableTill;
                                hasTuesday = true;
                                _availabilities.Add(Tuesday);
                                break;
                            case DayOfWeek.Wednesday:
                                Wednesday = new AvailabiltyVM(availability);
                                Wednesday.AvailabiltyStart = availability.AvailableFrom;
                                Wednesday.AvailabiltyEnd = availability.AvailableTill;
                                hasWednesday = true;
                                _availabilities.Add(Wednesday);
                                break;
                            case DayOfWeek.Thursday:
                                Thursday = new AvailabiltyVM(availability);
                                Thursday.AvailabiltyStart = availability.AvailableFrom;
                                Thursday.AvailabiltyEnd = availability.AvailableTill;
                                hasThursday = true;
                                _availabilities.Add(Thursday);
                                break;
                            case DayOfWeek.Friday:
                                Friday = new AvailabiltyVM(availability);
                                Friday.AvailabiltyStart = availability.AvailableFrom;
                                Friday.AvailabiltyEnd = availability.AvailableTill;
                                hasFriday = true;
                                _availabilities.Add(Friday);
                                break;
                            case DayOfWeek.Saturday:
                                Saturday = new AvailabiltyVM(availability);
                                Saturday.AvailabiltyStart = availability.AvailableFrom;
                                Saturday.AvailabiltyEnd = availability.AvailableTill;
                                hasSaturday = true;
                                _availabilities.Add(Saturday);
                                break;
                            case DayOfWeek.Sunday:
                                Sunday = new AvailabiltyVM(availability);
                                Sunday.AvailabiltyStart = availability.AvailableFrom;
                                Sunday.AvailabiltyEnd = availability.AvailableTill;
                                hasSunday = true;
                                _availabilities.Add(Sunday);
                                break;
                        }
                    }
                }
            }

            if (!hasMonday)
            {
                Monday = new AvailabiltyVM();
                _availabilities.Add(Monday);
            }
            if (!hasTuesday)
            {
                Tuesday = new AvailabiltyVM();
                _availabilities.Add(Tuesday);
            }
            if (!hasWednesday)
            {
                Wednesday = new AvailabiltyVM();
                _availabilities.Add(Wednesday);
            }
            if (!hasThursday)
            {
                Thursday = new AvailabiltyVM();
                _availabilities.Add(Thursday);
            }
            if (!hasFriday)
            {
                Friday = new AvailabiltyVM();
                _availabilities.Add(Friday);
            }
            if (!hasSaturday)
            {
                Saturday = new AvailabiltyVM();
                _availabilities.Add(Saturday);
            }
            if (!hasSunday)
            {
                Sunday = new AvailabiltyVM();
                _availabilities.Add(Sunday);
            }
        }

        private bool DatesAreInTheSameWeek(DateTime selectedWeek, DateTime allWeeks)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            if (cal != null)
            {
                var selWeek = selectedWeek.Date.AddDays(-1 * (int)cal.GetDayOfWeek(selectedWeek));
                var Allweek = allWeeks.Date.AddDays(-1 * (int)cal.GetDayOfWeek(allWeeks));
                return selWeek == Allweek;
            }
            else
            {
                MessageBox.Show("Er ging iets fout.");
            }

            return false;
        }
        public void SetCurrentWeekDates()
        {
            var availabilityManagerVM = CommonServiceLocator.ServiceLocator.Current.GetInstance<AvailabilityManagerVM>();
            foreach (var availability in _availabilities)
            {
                if (DatesAreInTheSameWeek(Week, availability.ToModel().AvailableFrom))
                {
                    switch (availability.ToModel().AvailableFrom.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            availabilityManagerVM.SetDate(Monday, 1);
                            break;
                        case DayOfWeek.Tuesday:
                            availabilityManagerVM.SetDate(Tuesday, 2);
                            break;
                        case DayOfWeek.Wednesday:
                            availabilityManagerVM.SetDate(Wednesday, 3);
                            break;
                        case DayOfWeek.Thursday:
                            availabilityManagerVM.SetDate(Thursday, 4);
                            break;
                        case DayOfWeek.Friday:
                            availabilityManagerVM.SetDate(Friday, 5);
                            break;
                        case DayOfWeek.Saturday:
                            availabilityManagerVM.SetDate(Saturday, 6);
                            break;
                        case DayOfWeek.Sunday:
                            availabilityManagerVM.SetDate(Sunday, 0);
                            break;
                    }
                }

            }
        }
    }
}
