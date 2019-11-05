using HousingOffersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services.Validators
{
    public class OfferGetRequestValidator : IOfferGetRequestValidator
    {
        private int distanceFromLocationLimit = 30;

        public string IsRequestValid(OffersRequestContentModel request)
        {
            if (request.MaxDistanceFromLocation > distanceFromLocationLimit)
                return "distance from location cannot be greater than 30km";

            return null;
        }
    }
}
