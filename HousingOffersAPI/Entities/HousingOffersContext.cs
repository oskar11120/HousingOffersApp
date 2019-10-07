using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Entities
{
    public class HousingOffersContext : DbContext
    {
        public HousingOffersContext(DbContextOptions<HousingOffersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
