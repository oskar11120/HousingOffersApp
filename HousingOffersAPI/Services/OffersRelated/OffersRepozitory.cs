using HousingOffersAPI.Entities;
using HousingOffersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services
{    
    public class OffersRepozitory : IOffersRepozitory
    {
        public OffersRepozitory(HousingOffersContext context)
        {
            this.context = context;
        }
        
        private HousingOffersContext context;

        public IEnumerable<Offer> GetOffers(offersRequestContentModel offersRequestContentModel)
        {
            //TODO handle querying for location
            var query = context.Offers.Where(offer => true);

            if(offersRequestContentModel.OfferId != null)
            {
                query = query.Where(offer => offer.Id == offersRequestContentModel.OfferId);
            }
            if (offersRequestContentModel.UserId != null)
            {
                query = query.Where(offer => offer.UserId == offersRequestContentModel.UserId);
            }
            if (offersRequestContentModel.PriceLimits != null)
            {
                query = query.Where(offer => offer.PriceInPLN > offersRequestContentModel.PriceLimits[0]
                && offer.PriceInPLN < offersRequestContentModel.PriceLimits[1]);
            }                    
            if (offersRequestContentModel.AreaLimits != null)
            {
                query = query.Where(offer => offer.Area > offersRequestContentModel.AreaLimits[0]
                && offer.Area < offersRequestContentModel.AreaLimits[1]);
            }
            if(offersRequestContentModel.PropertyTypes != null)
            {
                query = query.Where(offer => offersRequestContentModel.PropertyTypes
                .Any(propertyType => propertyType == offer.PropertyType));
            }
            if (offersRequestContentModel.OfferTypes != null)
            {
                query = query.Where(offer => offersRequestContentModel.OfferTypes
                .Any(offerType => offerType == offer.PropertyType));
            }

            return query;
        }
    }
}
