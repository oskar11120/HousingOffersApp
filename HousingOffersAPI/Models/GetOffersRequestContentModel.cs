using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models
{
    public class GetOffersRequestContentModel
    {
        public double[] PriceLimits { get; set; }
        public double[] AreaLimits { get; set; }
        public string[] PropertuTypes { get; set; }
        public string[] OfferTypes { get; set; }
        public string OfferId { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
    }
}
