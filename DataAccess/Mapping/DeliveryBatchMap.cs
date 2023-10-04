using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mapping;

public class DeliveryBatchMap : IEntityTypeConfiguration<DeliveryBatch>
{
    public void Configure(EntityTypeBuilder<DeliveryBatch> builder, string schema)
    {
        builder.Property(e => e.Date).HasColumnType("datetime");
    }
    public void Configure(EntityTypeBuilder<DeliveryBatch> builder)
    {
        Configure(builder, "dbo");
    }
}