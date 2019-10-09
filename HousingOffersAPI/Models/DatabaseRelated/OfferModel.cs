using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models
{
    public class OfferModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public double PriceInPLN { get; set; }
        public string Adress { get; set; }
        public double Area { get; set; }
        public string PropertyType { get; set; }
        public string OfferType { get; set; }        
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public IEnumerable<OfferTagModel> OfferTags { get; set; }
        public IEnumerable<ImageAdressModel> Images { get; set; }

    }
}
