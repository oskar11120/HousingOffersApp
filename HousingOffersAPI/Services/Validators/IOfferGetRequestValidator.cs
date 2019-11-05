using HousingOffersAPI.Models;

namespace HousingOffersAPI.Services.Validators
{
    public interface IOfferGetRequestValidator
    {
        string IsRequestValid(OffersRequestContentModel request);
    }
}