using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.Data.Mappers
{
    public class TripCategoryConfiguration : IEntityTypeConfiguration<TripCategory>
    {
        public void Configure(EntityTypeBuilder<TripCategory> builder)
        {
            builder.Property(tc => tc.Name).IsRequired();
        }

    }
}