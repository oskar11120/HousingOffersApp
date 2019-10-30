using HousingOffersAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Entities
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }
        public int OfferId { get; set; }

        public double Longitude { get; set; }
        public double Lattitue { get; set; }
        public string Description { get; set; }
    }
}
