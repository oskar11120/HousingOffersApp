using HousingOffersAPI.Models.DatabaseRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models.AnalyticsRelated
{
    public class UserClickModel
    {
        public int OfferId { get; set; }
        public DateTime DateTime { get; set; }
        public LocationModel Location { get; set; } 
    }
}
