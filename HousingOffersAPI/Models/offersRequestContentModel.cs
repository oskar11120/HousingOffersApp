using HousingOffersAPI.Models.DatabaseRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models
{
    public class OffersRequestContentModel
    {
        public double[] PriceLimits { get; set; }
        public double[] AreaLimits { get; set; }
        public DateTime[] DateTimeLimits { get; set; }
        public string[] PropertyTypes { get; set; }
        public string[] OfferTypes { get; set; }
        public int? OfferId { get; set; }
        public int? UserId { get; set; }
        public LocationModel Location { get; set; }
        public double MaxDistanceFromLocation { get; set; }
    }
}
