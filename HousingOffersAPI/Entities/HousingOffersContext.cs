using HousingOffersAPI.Models.AnalyticsRelated;
using HousingOffersAPI.Models.DatabaseRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Entities
{
    public class HousingOffersContext : DbContext
    {
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public HousingOffersContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        public HousingOffersContext(DbContextOptions<HousingOffersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<OfferClick> OfferClicks { get; set; }
        public DbSet<UserClick> UserClicks { get; set; }
    }
}
