using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Data.Mappers;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext 
    {

        public DbSet<Trip> Trips { get; set; }
        public DbSet<GeneralItem> Items { get; set; }
        public DbSet<GeneralCategory> Categories { get; set; }
        public DbSet<TripItem> TripItems { get; set; }
        public DbSet<TripCategory> TripCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new TripConfiguration());
            builder.ApplyConfiguration(new GeneralCategoryConfiguration());
            builder.ApplyConfiguration(new GeneralItemConfiguration());
            builder.ApplyConfiguration(new TripItemConfiguration());
            builder.ApplyConfiguration(new TripCategoryConfiguration());
            
        }
    }
}
