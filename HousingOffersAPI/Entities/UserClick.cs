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

        [ForeignKey("UserId")]
        public Offer User { get; set; }
        public int UserId { get; set; }
    }
}
