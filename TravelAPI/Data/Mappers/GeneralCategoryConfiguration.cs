using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.Data.Mappers
{
    public class GeneralCategoryConfiguration : IEntityTypeConfiguration<GeneralCategory>
    {
        public void Configure(EntityTypeBuilder<GeneralCategory> builder)
        {
            builder.Property(gc => gc.Name).IsRequired();
        }
    }
}
