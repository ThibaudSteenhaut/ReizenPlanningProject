using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Mappers
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.Property(t => t.Destination).IsRequired();
            builder.Property(t => t.DepartureDate).IsRequired();
            builder.Property(t => t.ReturnDate).IsRequired();
            builder.HasMany(t => t.Categories);

        }
    }
}
