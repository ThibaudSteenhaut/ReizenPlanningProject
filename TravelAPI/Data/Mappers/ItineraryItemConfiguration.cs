using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.Data.Mappers
{
    public class ItineraryItemConfiguration : IEntityTypeConfiguration<ItineraryItem>
    {
        public void Configure(EntityTypeBuilder<ItineraryItem> builder)
        {
            builder.Property(ii => ii.Description).IsRequired();
            builder.Property(ii => ii.Date).IsRequired();
        }
    }
}
