using Festispec.Domain;
using Festispec.ViewModel.customer.contactPerson;
using System;

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

        public Domain.Customer CustomerModel
        {
            get => _event.Customer;
            set => _event.Customer = value;
        }

        public ContactPerson ContactPersonModel
        {
            get => _event.ContactPerson;
            set => _event.ContactPerson = value;
        }


        public CustomerVM Customer {
            get => _customer;
            set
            {
               
                if (value == null) return;
                _customer = value;
                _event.CustomerId = value.Id;
                _event.Customer = value.ToModel();
            }
        }
        
        public ContactPersonVM ContactPerson {
            get => _contactPerson;
            set
            {
                
                if (value == null) return;
                _contactPerson = value;
                _event.ContactPersonId = value.Id;
                _event.ContactPerson = value.ToModel();
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
                return _event.BeginDate.Date;
            }
            set {
                _event.BeginDate = value.Date;
            }
        }

        public DateTime EndDate { 
            get {
                return _event.EndDate.Date;
            }
            set {
                _event.EndDate = value.Date;
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

        public string HouseNumberAddition
        {
            get => _event.HouseNumber_Addition;
            set => _event.HouseNumber_Addition = value;
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

        public Event ToModel()
        {
            return _event;
        }

        public EventVM()
        {
            _event = new Domain.Event();
            BeginDate = DateTime.Today.Date;
            EndDate = DateTime.Today.Date;
        }
    }
}
