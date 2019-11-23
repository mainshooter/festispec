using Festispec.Domain;
using Festispec.ViewModel.auth;
using System;
using System.Linq;

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
            bool HasMonday = false;
            bool HasTuesday = false;
            bool HasWednesday = false;
            bool HasThursday = false;
            bool HasFriday = false;
            bool HasSaturday = false;
            bool HasSunday = false;
            using (var context = new Entities())
            {
                var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
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
                                HasMonday = true;
                                break;
                            case DayOfWeek.Tuesday:
                                Tuesday = new AvailabiltyVM(availability);
                                Tuesday.AvailabiltyStart = availability.AvailableFrom;
                                Tuesday.AvailabiltyEnd = availability.AvailableTill;
                                HasTuesday = true;
                                break;
                            case DayOfWeek.Wednesday:
                                Wednesday = new AvailabiltyVM(availability);
                                Wednesday.AvailabiltyStart = availability.AvailableFrom;
                                Wednesday.AvailabiltyEnd = availability.AvailableTill;
                                HasWednesday = true;
                                break;
                            case DayOfWeek.Thursday:
                                Thursday = new AvailabiltyVM(availability);
                                Thursday.AvailabiltyStart = availability.AvailableFrom;
                                Thursday.AvailabiltyEnd = availability.AvailableTill;
                                HasThursday = true;
                                break;
                            case DayOfWeek.Friday:
                                Friday = new AvailabiltyVM(availability);
                                Friday.AvailabiltyStart = availability.AvailableFrom;
                                Friday.AvailabiltyEnd = availability.AvailableTill;
                                HasFriday = true;
                                break;
                            case DayOfWeek.Saturday:
                                Saturday = new AvailabiltyVM(availability);
                                Saturday.AvailabiltyStart = availability.AvailableFrom;
                                Saturday.AvailabiltyEnd = availability.AvailableTill;
                                HasSaturday = true;
                                break;
                            case DayOfWeek.Sunday:
                                Sunday = new AvailabiltyVM(availability);
                                Sunday.AvailabiltyStart = availability.AvailableFrom;
                                Sunday.AvailabiltyEnd = availability.AvailableTill;
                                HasSunday = true;
                                break;
                        }
                    }
                }
            }

            if (!HasMonday)
            {
                Monday = new AvailabiltyVM();
            }
            if (!HasTuesday)
            {
                Tuesday = new AvailabiltyVM();
            }
            if (!HasWednesday)
            {
                Wednesday = new AvailabiltyVM();
            }
            if (!HasThursday)
            {
                Thursday = new AvailabiltyVM();
            }
            if (!HasFriday)
            {
                Friday = new AvailabiltyVM();
            }
            if (!HasSaturday)
            {
                Saturday = new AvailabiltyVM();
            }
            if (!HasSunday)
            {
                Sunday = new AvailabiltyVM();
            }
        }

        private bool DatesAreInTheSameWeek(DateTime SelectedWeek, DateTime AllWeeks)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var selWeek = SelectedWeek.Date.AddDays(-1 * (int)cal.GetDayOfWeek(SelectedWeek));
            var Allweek = AllWeeks.Date.AddDays(-1 * (int)cal.GetDayOfWeek(AllWeeks));

            return selWeek == Allweek;
        }
    }
}
