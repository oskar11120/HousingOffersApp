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
        public string Adress { get; set; }
        public double Area { get; set; }
        public string PropertyType { get; set; }
        public string OfferType { get; set; }
        public string ActionType { get; set; }        
        public string Description { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }

        public ICollection<OfferTag> OfferTags { get; set; }
        public ICollection<ImageAdress> Images { get; set; }
    }
}
