using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping;

public class ProductGroupMap : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder, string schema)
    {
        builder.HasOne(d => d.Location).WithMany(p => p.ProductGroups)
               .HasForeignKey(d => d.LocationId)
               .HasConstraintName("FK_ProductGroups_Locations");
    }
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        Configure(builder, "dbo");
    }
}