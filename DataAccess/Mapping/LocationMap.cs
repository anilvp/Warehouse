using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mapping;

public class LocationMap : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder, string schema)
    {
        builder.Property(e => e.Code)
               .HasMaxLength(10)
               .IsFixedLength();

        builder.HasOne(d => d.DefaultProduct).WithMany(p => p.Locations)
               .HasForeignKey(d => d.DefaultProductId)
               .HasConstraintName("FK_Locations_Products");

        builder.HasOne(d => d.LocationType).WithMany(p => p.Locations)
               .HasForeignKey(d => d.LocationTypeId)
               .HasConstraintName("FK_Locations_LocationTypes");
    }
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        Configure(builder, "dbo");
    }
}