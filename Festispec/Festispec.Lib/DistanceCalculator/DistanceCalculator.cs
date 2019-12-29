using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistanceCalculator
{
    public class DistanceCalculator
    {

        private static string bingMapKey = "ITBT6VEQtWQIP2Nt0sEo~GvONPsboTGlj2F7N16RX1Q~AuuacnuDtvdGIIkJJsITb1P2J1Cr0vNjAgb3KKNYIiUUa2su3wzy_67N4XpmXffL";

        //Gebaseerd op twee SimpleWaypoints wordt de afstand berekent tussen die twee punten een een double van die afstand in kilometers teruggegeven
        public async Task<double> TravelDistance(SimpleWaypoint originCoor, SimpleWaypoint destinationCoor)
        {

            List<SimpleWaypoint> origin = new List<SimpleWaypoint>();
            List<SimpleWaypoint> destination = new List<SimpleWaypoint>();

            origin.Add(originCoor);
            destination.Add(destinationCoor);

            DistanceMatrixRequest distance = new DistanceMatrixRequest();

            distance.Origins = origin;
            distance.Destinations = destination;
            distance.BingMapsKey = bingMapKey;
            distance.TravelMode = TravelModeType.Driving;

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

        //Gebaseerd op adres, stad en postcode returned deze methode een SimpleWaypoint van die plek in longtitude en latitude
        public async Task<SimpleWaypoint> AdressToCoor(string street, string houseNumber, string city ,string postalCode)
        {
            GeocodeRequest geocode = new GeocodeRequest();
            SimpleAddress address = new SimpleAddress();

            address.AddressLine = street + houseNumber;
            address.Locality = city;
            address.PostalCode = postalCode;

            geocode.Address = address;
            geocode.BingMapsKey = bingMapKey;

            var response = await geocode.Execute();

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                Location result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;

                if (result.GeocodePoints.Length == 1) return null;

                SimpleWaypoint waypoint = new SimpleWaypoint(result.GeocodePoints[1].Coordinates[0], result.GeocodePoints[1].Coordinates[1]);
                return waypoint;
            }

            return null;
        }
    }
}
