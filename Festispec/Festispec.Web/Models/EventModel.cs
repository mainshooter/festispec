using Festispec.Domain;

namespace Festispec.Web.Models
{
    public class EventModel
    {
        public Event Event { get; set; }
        public string ContactName
        {
            get
            {
                if (Event.ContactPerson.Prefix != null)
                {
                    return Event.ContactPerson.Firstname + " " + Event.ContactPerson.Prefix + " " + Event.ContactPerson.Lastname;
                }
                return Event.ContactPerson.Firstname + " " + Event.ContactPerson.Lastname;
            }
        }

        public string Adress 
        {
            get
            {
                return Event.Street + " " + Event.HouseNumber + Event.HouseNumber_Addition;
            }
        }

    }
}