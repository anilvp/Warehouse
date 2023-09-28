using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping;

public class ProductGroupContentMap : IEntityTypeConfiguration<ProductGroupContent>
{
    public void Configure(EntityTypeBuilder<ProductGroupContent> builder, string schema)
    {
        builder.HasKey(e => new { e.ProductGroupId, e.ProductId, e.DeliveryBatchId }).HasName("PK_ProductGroupProducts");

        builder.HasOne(d => d.ProductGroup).WithMany(p => p.ProductGroupContents)
               .HasForeignKey(d => d.ProductGroupId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_ProductGroupProducts_ProductGroups");

        builder.HasOne(d => d.DeliveryBatchContent).WithMany(p => p.ProductGroupContents)
               .HasForeignKey(d => new { d.ProductId, d.DeliveryBatchId })
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_ProductGroupProducts_PoductDeliveryBatches");
    }
    public void Configure(EntityTypeBuilder<ProductGroupContent> builder)
    {
        Configure(builder, "dbo");
    }
}