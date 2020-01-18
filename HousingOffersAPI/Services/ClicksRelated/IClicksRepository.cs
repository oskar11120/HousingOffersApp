using System.Collections.Generic;
using HousingOffersAPI.Models.AnalyticsRelated;

namespace HousingOffersAPI.Services.ClicksRelated
{
    public interface IClicksRepository
    {
        void AddOfferClick(int offerId);
        void AddUserClick(int userId);
        List<OfferClickModel> GetOfferClicks();
        List<UserClickModel> GetUserClicks();
    }
}