using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping;

public class LocationTypeMap : IEntityTypeConfiguration<LocationType>
{
    public void Configure(EntityTypeBuilder<LocationType> builder, string schema)
    {
        builder.HasKey(e => e.LocationTypeId).HasName("PK__Location__737D32F9C9B41B8E");

        builder.Property(e => e.LocationType1)
               .HasMaxLength(30)
               .IsUnicode(false)
               .HasColumnName("LocationType");
    }
    public void Configure(EntityTypeBuilder<LocationType> builder)
    {
        Configure(builder, "dbo");
    }
}