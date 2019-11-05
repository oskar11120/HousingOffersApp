using HousingOffersAPI.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services.Validators
{
    public class OfferValidator : IOfferValidator
    {
        public OfferValidator(IOptions<Dictionary<string, List<string>>> options)
        {
            this.allowedOfferTypes = options.Value["OfferTypes"];
            this.allowedProperyTypes = options.Value["PropertyTypes"];
        }
        private readonly IEnumerable<string> allowedProperyTypes;
        private readonly IEnumerable<string> allowedOfferTypes;

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
            return null;
        }
    }
}
