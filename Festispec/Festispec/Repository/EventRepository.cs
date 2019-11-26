using Festispec.Domain;
using Festispec.ViewModel.customer.customerEvent;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.Repository
{
    public class EventRepository
    {
        public List<EventVM> GetEvents()
        {
            List<EventVM> result = new List<EventVM>();
            using (var context = new Entities())
            {
                result = new List<EventVM>(context.Events.ToList().Select(eventCon => new EventVM(eventCon)));
            }
            return result;
        }
    }
}
