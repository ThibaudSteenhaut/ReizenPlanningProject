using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data
{
    public class TripContext:DbContext
    {
        public TripContext(DbContextOptions<TripContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Trip>();

            builder.Entity<Trip>().Property(t => t.destination).IsRequired();

            builder.Entity<Trip>().HasData(
                new Trip() { destination = "Rome", DepartureDate = new DateTime(2020, 12, 30), ReturnDate = new DateTime(2021, 1, 10) },
                    new Trip() { destination = "Parijs", DepartureDate = new DateTime(2021, 1, 10), ReturnDate = new DateTime(2021, 2, 10) },
                    new Trip() { destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) },
                    new Trip() { destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) },
                    new Trip() { destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) },
                    new Trip() { destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) },
                    new Trip() { destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) },
                    new Trip() { destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) },
                );
        }
        public DbSet<Trip> trips { get; set; }
    }
}
