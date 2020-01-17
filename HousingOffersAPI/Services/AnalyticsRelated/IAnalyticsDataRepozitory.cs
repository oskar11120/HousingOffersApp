using HousingOffersAPI.Models.AnalyticsRelated;

namespace HousingOffersAPI.Services.AnalyticsRelated
{
    public interface IAnalyticsDataRepozitory
    {
        void AddContactRequest(UserClickModel userContanctRequestAnalyticsModel);
        void AddOfferRequest(OfferClickModel userOfferRequestAnalyticsModel);
    }
}