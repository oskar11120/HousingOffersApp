using HousingOffersAPI.Options;
using HousingOffersAPI.Services.ScriptRelated;
using HousingOffersAPI.Services.TaskRelated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HousingOffersAPI.Services.RecommendationRelated
{    
    public class RecommendationRepository : IRecommendationRepository
    {
        public RecommendationRepository(ApiOptions options, IOffersRepozitory offersRepozitory)
        {            
            update();
            this.options = options.RecomendationOptions;
        }

        private readonly RecomendationOptions options;

        private List<Tuple<int, double>> recommendations = new List<Tuple<int, double>>();

        public List<int> GetIdsOfRecommended(int idToGetRecommendations, int amount)
        {
            var valueToLookFor = recommendations.Single(recom => recom.Item1 == idToGetRecommendations).Item2;
            int count = recommendations.Count();
            if (amount < count)
                count = amount;

            return recommendations.OrderBy(recom => Math.Abs(recom.Item1 - valueToLookFor))
                .Select(recom => recom.Item1)
                .ToList()
                .GetRange(1, count);                
        }

        private void update()
        {
            readFromCsv(options.RecomendationFilePath, ',');
        }
        private void readFromCsv(string path, char delimeter)
        {
            var lines = File.ReadAllLines(path).Select(a => a.Split(delimeter))
                .ToList();
            lines.ForEach(line =>
            {
                recommendations.Add(new Tuple<int, double>(int.Parse(line[0]), double.Parse(line[1])));
            });
        }
        
    }
}
