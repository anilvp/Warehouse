using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mapping;

public class ProductGroupMap : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder, string schema)
    {
        builder.HasIndex(e => e.LocationId, "UQ__ProductG__E7FEA4968843BBE7").IsUnique();

        builder.HasOne(d => d.Location).WithOne(p => p.ProductGroup)
               .HasForeignKey<ProductGroup>(d => d.LocationId)
               .HasConstraintName("FK_ProductGroups_Locations");
    }
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        Configure(builder, "dbo");
    }
}