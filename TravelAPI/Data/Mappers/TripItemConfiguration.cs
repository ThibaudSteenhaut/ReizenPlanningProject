using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Mappers
{
    public class TripItemConfiguration : IEntityTypeConfiguration<TripItem>
    {
        public void Configure(EntityTypeBuilder<TripItem> builder)
        {
            builder.Property(ti => ti.Amount).IsRequired();
            builder.Property(ti => ti.CheckedIn).IsRequired();
            builder.HasOne(ti => ti.Item);
        }
    }
}
