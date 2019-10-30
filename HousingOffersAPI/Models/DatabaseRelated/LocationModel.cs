using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models.DatabaseRelated
{
    public class LocationModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }

        public double Longitude { get; set; }
        public double Lattitue { get; set; }
        public string Description { get; set; }
    }
}
