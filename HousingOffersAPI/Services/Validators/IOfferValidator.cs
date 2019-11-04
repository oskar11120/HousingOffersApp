using HousingOffersAPI.Models;

namespace HousingOffersAPI.Services.Validators
{
    public interface IOfferValidator
    {
        //returns error message if error appears and null if there is no error
        string IsOfferValid(OfferModel offer);
    }
}