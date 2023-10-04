using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder, string schema)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(10)
               .IsFixedLength();
    }
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        Configure(builder, "dbo");
    }
}