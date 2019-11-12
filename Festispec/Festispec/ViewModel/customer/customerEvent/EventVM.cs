using BingMapsRESTToolkit;
using Festispec.Domain;
using Festispec.ViewModel.customer.contactPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.customer.customerEvent
{
    public class EventVM
    {
        private Event _event;
        private CustomerVM _customer;
        private ContactPersonVM _contactPerson;

        public int Id { 
            get {
                return _event.Id;
            }
            private set {
                _event.Id = value;
            }
        }
        
        public CustomerVM Customer { 
            get {
                return _customer;
            }
            set {
                _customer = value;
            }
        }
        
        public ContactPersonVM ContactPerson { 
            get {
                return _contactPerson;
            }
            set {
                _contactPerson = value;
            }
        }

        public string Name { 
            get {
                return _event.Name;
            }
            set {
                _event.Name = value;
            }
        }

        public DateTime BeginDate { 
            get {
                return _event.BeginDate;
            }
            set {
                _event.BeginDate = value;
            }
        }

        public DateTime EndDate { 
            get {
                return _event.EndDate;
            }
            set {
                _event.EndDate = value;
            }
        }

        public int AmountVisitors { 
            get {
                return _event.AmountVisitors;
            }
            set {
                _event.AmountVisitors = value;
            }
        }

        public int SurfaceM2 { 
            get {
                return _event.SurfaceM2;
            }
            set {
                _event.SurfaceM2 = value;
            }
        }

        public string Description { 
            get {
                return _event.Description;
            }
            set {
                _event.Description = value;
            }
        }

        public string Street
        {
            get
            {
                return _event.Street;
            }
            set
            {
                _event.Street = value;
            }
        }

        public int HouseNumber
        {
            get
            {
                return _event.HouseNumber;
            }
            set
            {
                _event.HouseNumber = value;
            }
        }

        public string PostalCode
        {
            get
            {
                return _event.PostalCode;
            }
            set
            {
                _event.PostalCode = value;
            }
        }

        public string City
        {
            get
            {
                return _event.City;
            }
            set
            {
                _event.City = value;
            }
        }

        public EventVM(Event eventCon)
        {
            _event = eventCon;
            _customer = new CustomerVM(_event.Customer);
            _contactPerson = new ContactPersonVM(_event.ContactPerson);
        }

        public EventVM()
        {
            _event = new Domain.Event();
        }

        public async Task<SimpleWaypoint> AdressToCoor()
        {
            GeocodeRequest geocode = new GeocodeRequest();
            SimpleAddress address = new SimpleAddress();

            address.AddressLine = Street + HouseNumber;
            address.Locality = City;
            address.PostalCode = PostalCode;

            geocode.Address = address;
            geocode.BingMapsKey = "ITBT6VEQtWQIP2Nt0sEo~GvONPsboTGlj2F7N16RX1Q~AuuacnuDtvdGIIkJJsITb1P2J1Cr0vNjAgb3KKNYIiUUa2su3wzy_67N4XpmXffL";

            var response = await geocode.Execute();

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                Location result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;
                SimpleWaypoint waypoint = new SimpleWaypoint(result.GeocodePoints[1].Coordinates[0], result.GeocodePoints[1].Coordinates[1]);
                return waypoint;
            }

            return null;
        }
    }
}
