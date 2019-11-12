using BingMapsRESTToolkit;
using Festispec.Domain;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee
{
    public class EmployeeVM
    {
        private DepartmentVM _department;
        private Employee _employee;

        public int Id {
            get {
                return _employee.Id;
            }
            private set {
                _employee.Id = value;
            }
        }
        
        public DepartmentVM Department { 
            get {
                return _department;
            }
            set {
                _department = value;
            }
        }

        public string Firstname { 
            get {
                return _employee.Firstname;
            }
            set {
                _employee.Firstname = value;
            }
        }

        public string Lastname { 
            get {
                return _employee.Lastname;
            }
            set {
                _employee.Lastname = value;
            }
        }

        public string Street { 
            get {
                return _employee.Street;
            }
            set {
                _employee.Street = value;
            }
        }

        public int HouseNumber {
            get {
                return _employee.HouseNumber;
            }
            set {
                _employee.HouseNumber = value;
            }
        }

        public string PostalCode { 
            get {
                return _employee.PostalCode;
            }
            set {
                _employee.PostalCode = value;
            }
        }

        public string City {
            get {
                return _employee.City;
            }
            set {
                _employee.City = value;
            }
        }

        public string Email { 
            get {
                return _employee.Email;
            }
            set {
                _employee.Email = value;
            }
        }

        public string Phone { 
            get {
                return _employee.Phone;
            }
            set {
                _employee.Phone = value;
            }
        }

        public string Password { 
            get {
                return _employee.Password;
            }
            set {
                _employee.Password = value;
            }
        }

        private string PasswordResetToken { 
            get {
                return _employee.PasswordResetToken;
            }
            set {
                _employee.PasswordResetToken = value;
            }
        }

        public DateTime ResetTokenEndTime {
            get {
                return (DateTime) _employee.ResetTokenEndTime;
            }
            set {
                _employee.ResetTokenEndTime = value;
            }
        }

        public string Iban { 
            get {
                return _employee.Iban;
            }
            set {
                _employee.Iban = value;
            }
        }

        public string Status {
            get {
                return _employee.Status;
            }
            set {
                _employee.Status = value;
            }
        }

        public EmployeeVM(Employee employee)
        {
            _employee = employee;
            Department = new DepartmentVM(_employee.Department1);
        }

        public EmployeeVM()
        {
            _employee = new Employee();
        }

        public async Task<double> TravelDistance(SimpleWaypoint eventCoor){

            List<SimpleWaypoint> origin = new List<SimpleWaypoint>();
            List<SimpleWaypoint> destination = new List<SimpleWaypoint>();

            origin.Add(await AdressToCoor());
            destination.Add(eventCoor);

            DistanceMatrixRequest distance = new DistanceMatrixRequest();

            distance.Origins = origin;
            distance.Destinations = destination;
            distance.BingMapsKey = "ITBT6VEQtWQIP2Nt0sEo~GvONPsboTGlj2F7N16RX1Q~AuuacnuDtvdGIIkJJsITb1P2J1Cr0vNjAgb3KKNYIiUUa2su3wzy_67N4XpmXffL";
            distance.DistanceUnits = DistanceUnitType.Kilometers;

            var response = await distance.Execute();

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                DistanceMatrix result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.DistanceMatrix;
                return result.Results[0].TravelDistance;
            }
            return -1;
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
