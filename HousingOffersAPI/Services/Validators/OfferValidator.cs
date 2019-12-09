using HousingOffersAPI.Models;
using HousingOffersAPI.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services.Validators
{
    public class OfferValidator : IOfferValidator
    {
        public OfferValidator(IOptions<ApiOptions> options, IOffersRepozitory offersRepozitory)
        {
            this.allowedOfferTypes = options.Value.OffersControllerOptions.AllowedValues["OfferTypes"];
            this.allowedProperyTypes = options.Value.OffersControllerOptions.AllowedValues["PropertyTypes"];
            this.offersRepozitory = offersRepozitory;
        }
        private readonly IEnumerable<string> allowedProperyTypes;
        private readonly IEnumerable<string> allowedOfferTypes;
        private readonly IOffersRepozitory offersRepozitory;

        private readonly int descLengthLimit = 1000;

        public string IsOfferValid(OfferModel offer)
        {
            if (!this.allowedProperyTypes.Any(allowedPropertyType => allowedPropertyType == offer.PropertyType))
                return "unallowed property type!";
            if (!this.allowedOfferTypes.Any(allowedOfferType => allowedOfferType == offer.OfferType))
                return "unallowed offer type!";
            if (offer.Location == null)
                return "invalid location";
            if (offer.PriceInPLN <= 0)
                return "invalid price specified!";
            if (offer.Images.Count() == 0 || offer.Images == null)
                return "atleast one image needed!";
            if (offer.Area <= 0)
                return "invalid offer area!";
            if (offer.Description.Count() > descLengthLimit)
                return "description too long!";
            //if()  todo limit amount of offers for user
            return null;
        }
    }
}
