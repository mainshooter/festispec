using System;

namespace Festispec.Lib
{
    public static class DateHelpers
    {
        public static bool DatesAreInTheSameWeek(DateTime date)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date));
            var d2 = DateTime.Today.Date.AddDays(-1 * (int)cal.GetDayOfWeek(DateTime.Today));

            return d1 == d2;
        }
    }
}
