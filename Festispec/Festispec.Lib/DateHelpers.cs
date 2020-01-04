using System;

namespace Festispec.Lib
{
    public static class DateHelpers
    {
        public static bool DatesAreInTheSameWeek(DateTime date)
        {
           return (date - DateTime.Today).TotalDays <= 7;
        }
    }
}
