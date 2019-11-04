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

        public string IsOfferValid(OfferModel offer)
        {
            if (!this.allowedProperyTypes.Any(allowedPropertyType => allowedPropertyType == offer.PropertyType))
                return "unallowed property type!";
            if (!this.allowedOfferTypes.Any(allowedOfferType => allowedOfferType == offer.OfferType))
                return "unallowed offer type!";

            return null;
        }
    }
}
