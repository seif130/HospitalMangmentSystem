using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Configurations;

internal sealed class PurchaseRequestConfiguration : IEntityTypeConfiguration<PurchaseRequest>
{
    public void Configure(EntityTypeBuilder<PurchaseRequest> builder)
    {
        builder.ToTable("PurchaseRequests");
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.PurchaseRequestId());
        builder.Property(x => x.DepartmentId).HasConversion(StrongIdValueConverters.DepartmentId()).IsRequired();
        builder.Property(x => x.Reason).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();

        builder.OwnsMany(x => x.Lines, line =>
        {
            line.ToTable("PurchaseRequestLines");
            line.Property<Guid>("Id");
            line.HasKey("Id");
            line.Property(x => x.ItemName).HasMaxLength(300).IsRequired();
            line.Property(x => x.Quantity).IsRequired();
            line.OwnsOne(x => x.EstimatedUnitPrice, money =>
            {
                money.Property(x => x.Amount).HasColumnName("EstimatedUnitPriceAmount").HasPrecision(18, 2).IsRequired();
                money.Property(x => x.Currency).HasColumnName("EstimatedUnitPriceCurrency").HasMaxLength(3).IsRequired();
            });
            line.Ignore(x => x.EstimatedTotal);
        });

        builder.Property(x => x.CreatedBy).HasMaxLength(100);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        builder.Property(x => x.DeletedBy).HasMaxLength(100);

        builder.HasIndex(x => x.DepartmentId);
        builder.HasIndex(x => new { x.DepartmentId, x.Status });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
