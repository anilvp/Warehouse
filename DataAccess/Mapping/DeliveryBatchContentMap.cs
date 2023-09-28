using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping;

public class DeliveryBatchContentMap : IEntityTypeConfiguration<DeliveryBatchContent>
{
    public void Configure(EntityTypeBuilder<DeliveryBatchContent> builder, string schema)
    {
        builder.HasKey(e => new { e.ProductId, e.DeliveryBatchId }).HasName("PK_PoductDeliveryBatches");

        builder.HasOne(d => d.DeliveryBatch).WithMany(p => p.DeliveryBatchContents)
               .HasForeignKey(d => d.DeliveryBatchId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_PoductDeliveryBatches_DeliveryBatches");

        builder.HasOne(d => d.Product).WithMany(p => p.DeliveryBatchContents)
               .HasForeignKey(d => d.ProductId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_PoductDeliveryBatches_Products");
    }
    public void Configure(EntityTypeBuilder<DeliveryBatchContent> builder)
    {
        Configure(builder, "dbo");
    }
}