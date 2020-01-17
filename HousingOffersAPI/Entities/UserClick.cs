using HousingOffersAPI.Entities;
using HousingOffersAPI.Models.DatabaseRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models.AnalyticsRelated
{
    public class UserClick
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }
        public int OfferId { get; set; }
    }
}
