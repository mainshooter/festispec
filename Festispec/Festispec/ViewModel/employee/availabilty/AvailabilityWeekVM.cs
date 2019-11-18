using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Monday = new AvailabiltyVM();
            Tuesday = new AvailabiltyVM();
            Wednesday = new AvailabiltyVM();
            Thursday = new AvailabiltyVM();
            Friday = new AvailabiltyVM();
            Saturday = new AvailabiltyVM();
            Sunday = new AvailabiltyVM();
        }

    }
}
