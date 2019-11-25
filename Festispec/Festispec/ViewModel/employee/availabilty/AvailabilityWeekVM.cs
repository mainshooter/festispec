using Festispec.Domain;
using Festispec.ViewModel.auth;
using System;
using System.Linq;
using System.Windows;

namespace Festispec.ViewModel.employee.availabilty
{
    public class AvailabilityWeekVM
    {
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
                                break;
                            case DayOfWeek.Tuesday:
                                Tuesday = new AvailabiltyVM(availability);
                                Tuesday.AvailabiltyStart = availability.AvailableFrom;
                                Tuesday.AvailabiltyEnd = availability.AvailableTill;
                                hasTuesday = true;
                                break;
                            case DayOfWeek.Wednesday:
                                Wednesday = new AvailabiltyVM(availability);
                                Wednesday.AvailabiltyStart = availability.AvailableFrom;
                                Wednesday.AvailabiltyEnd = availability.AvailableTill;
                                hasWednesday = true;
                                break;
                            case DayOfWeek.Thursday:
                                Thursday = new AvailabiltyVM(availability);
                                Thursday.AvailabiltyStart = availability.AvailableFrom;
                                Thursday.AvailabiltyEnd = availability.AvailableTill;
                                hasThursday = true;
                                break;
                            case DayOfWeek.Friday:
                                Friday = new AvailabiltyVM(availability);
                                Friday.AvailabiltyStart = availability.AvailableFrom;
                                Friday.AvailabiltyEnd = availability.AvailableTill;
                                hasFriday = true;
                                break;
                            case DayOfWeek.Saturday:
                                Saturday = new AvailabiltyVM(availability);
                                Saturday.AvailabiltyStart = availability.AvailableFrom;
                                Saturday.AvailabiltyEnd = availability.AvailableTill;
                                hasSaturday = true;
                                break;
                            case DayOfWeek.Sunday:
                                Sunday = new AvailabiltyVM(availability);
                                Sunday.AvailabiltyStart = availability.AvailableFrom;
                                Sunday.AvailabiltyEnd = availability.AvailableTill;
                                hasSunday = true;
                                break;
                        }
                    }
                }
            }

            if (!hasMonday)
            {
                Monday = new AvailabiltyVM();
            }
            if (!hasTuesday)
            {
                Tuesday = new AvailabiltyVM();
            }
            if (!hasWednesday)
            {
                Wednesday = new AvailabiltyVM();
            }
            if (!hasThursday)
            {
                Thursday = new AvailabiltyVM();
            }
            if (!hasFriday)
            {
                Friday = new AvailabiltyVM();
            }
            if (!hasSaturday)
            {
                Saturday = new AvailabiltyVM();
            }
            if (!hasSunday)
            {
                Sunday = new AvailabiltyVM();
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
    }
}
