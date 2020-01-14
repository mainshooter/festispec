using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Map;
using GalaSoft.MvvmLight;
using Microsoft.Maps.MapControl.WPF;

namespace Festispec.ViewModel.Map
{
    public class MapVM : ViewModelBase
    {
        private List<Employee> _employees;
        private List<Domain.Customer> _customers;
        private List<Event> _events;
        private readonly DistanceCalculator.DistanceCalculator _distanceCalculator;

        public ObservableCollection<Pushpin> PushPins { get; set; }

        public MapVM()
        {
            _distanceCalculator = new global::DistanceCalculator.DistanceCalculator();
            FillLists();
            CreatePushPins();

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(MapPage))
                {
                    FillLists();
                    CreatePushPins();
                }
            });
        }

        private void FillLists()
        {
            using (var context = new Entities())
            {
                _employees = context.Employees.ToList();
                _customers = context.Customers.ToList();
                _events = context.Events.ToList();
            }
        }

        private void CreatePushPins()
        {
            PushPins = new ObservableCollection<Pushpin>();

            foreach (var employee in _employees)
            {
                if (employee.Department == "Inspectie")
                {
                    CreatePushPin(employee.Firstname + " " + employee.Prefix + " " + employee.Lastname, employee.Department, employee.Street, employee.HouseNumber + employee.HouseNumber_Addition, employee.City, employee.PostalCode, Colors.Red);
                }
            }

            foreach (var customer in _customers)
            {
                CreatePushPin(customer.Name, "K", customer.Street, customer.HouseNumber + customer.HouseNumber_Addition, customer.City, customer.PostalCode, Colors.Green);
            }

            foreach (var inspection in _events)
            {
                CreatePushPin(inspection.Name, "E", inspection.Street, inspection.HouseNumber + inspection.HouseNumber_Addition, inspection.City, inspection.PostalCode, Colors.DeepSkyBlue);
            }
        }

        private async void CreatePushPin(string name, string type, string street, string houseNumber, string city, string postalCode, Color color)
        {
            try
            {
                var pin = new Pushpin { Content = type.ToCharArray()[0], Background = new SolidColorBrush(color) };
                var tooltip = new ToolTip { Content = name };
                var location = await _distanceCalculator.AdressToCoor(street, houseNumber, city, postalCode);
                pin.Location = new Location(location.Coordinate.Latitude, location.Coordinate.Longitude);
                pin.ToolTip = tooltip;
                PushPins.Add(pin);
                RaisePropertyChanged(() => PushPins);
            }
            catch (System.Exception)
            {

            }

        }
    }
}
