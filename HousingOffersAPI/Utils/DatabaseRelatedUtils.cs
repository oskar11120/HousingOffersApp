using HousingOffersAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocation;

namespace HousingOffersAPI.Utils
{
    public static class DatabaseRelatedUtils
    {
        public static IEnumerable<double> getDistances(Location referenceLocation, List<Location> locations)
        {
            var referencePoint = new Coordinate()
            {
                Latitude = referenceLocation.Lattitue,
                Longitude = referenceLocation.Longitude
            };

            return locations.Select(location => GeoCalculator.GetDistance(referencePoint, new Coordinate()
            {
                Latitude = location.Lattitue,
                Longitude = location.Longitude
            }));
        }
    }
}
