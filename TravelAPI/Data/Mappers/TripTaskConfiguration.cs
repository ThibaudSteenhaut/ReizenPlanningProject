using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.Data.Mappers
{
    public class TripTaskConfiguration : IEntityTypeConfiguration<TripTask>
    {
        public void Configure(EntityTypeBuilder<TripTask> builder)
        {
            builder.Property(tt => tt.Description).IsRequired();
        }
    }
}
