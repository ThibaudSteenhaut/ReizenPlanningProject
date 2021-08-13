using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Mappers
{
    public class GeneralItemConfiguration : IEntityTypeConfiguration<GeneralItem>
    {
        public void Configure(EntityTypeBuilder<GeneralItem> builder)
        {
            builder.Property(i => i.Name).IsRequired();
        }
    }
}
