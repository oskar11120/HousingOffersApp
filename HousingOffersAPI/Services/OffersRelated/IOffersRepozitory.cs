using System.Collections.Generic;
using HousingOffersAPI.Entities;
using HousingOffersAPI.Models;

namespace HousingOffersAPI.Services
{
    public interface IOffersRepozitory
    {
        IEnumerable<Offer> GetOffers(offersRequestContentModel offersRequestContentModel);
    }
}