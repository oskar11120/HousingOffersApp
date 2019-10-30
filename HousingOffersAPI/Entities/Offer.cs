using HousingOffersAPI.Models.DatabaseRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Entities
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double PriceInPLN { get; set; }
        public Location Location { get; set; }
        public double Area { get; set; }
        public string PropertyType { get; set; }
        public string OfferType { get; set; }       
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }

        public IEnumerable<OfferTag> OfferTags { get; set; }
        public IEnumerable<ImageAdress> Images { get; set; }
    }
}
