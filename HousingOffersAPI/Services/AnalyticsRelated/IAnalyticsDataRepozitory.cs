using HousingOffersAPI.Models.AnalyticsRelated;

namespace HousingOffersAPI.Services.AnalyticsRelated
{
    public interface IAnalyticsDataRepozitory
    {
        void AddContactRequest(UserContactRequestAnalyticsModel userContanctRequestAnalyticsModel);
        void AddOfferRequest(UserOfferRequestAnalyticsModel userOfferRequestAnalyticsModel);
    }
}