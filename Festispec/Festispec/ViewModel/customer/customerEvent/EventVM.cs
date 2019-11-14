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

        public EventVM(Event eventCon)
        {
            _event = eventCon;
            //_customer = new CustomerVM(_event.Customer);
            //_contactPerson = new ContactPersonVM(_event.ContactPerson);
        }

        public EventVM()
        {
            _event = new Domain.Event();
        }
    }
}
