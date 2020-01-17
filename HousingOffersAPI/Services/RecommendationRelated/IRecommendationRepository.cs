using System.Collections.Generic;

namespace HousingOffersAPI.Services.RecommendationRelated
{
    public interface IRecommendationRepository
    {
        List<int> GetIdsOfRecommended(int idToGetRecommendations, int amount);
    }
}