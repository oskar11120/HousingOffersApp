using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingOffersAPI.Entities
{
    public class OfferTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }
        public int OfferId { get; set; }
    }
}