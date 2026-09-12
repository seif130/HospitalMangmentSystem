using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Configurations;

internal sealed class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("PurchaseOrders");
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.PurchaseOrderId());
        builder.Property(x => x.VendorId).HasConversion(StrongIdValueConverters.VendorId()).IsRequired();
        builder.Property(x => x.PurchaseRequestId).HasConversion(StrongIdValueConverters.PurchaseRequestId());
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();

        builder.OwnsOne(x => x.TotalAmount, money =>
        {
            money.Property(x => x.Amount).HasColumnName("TotalAmount").HasPrecision(18, 2).IsRequired();
            money.Property(x => x.Currency).HasColumnName("TotalCurrency").HasMaxLength(3).IsRequired();
        });

        builder.OwnsMany(x => x.Lines, line =>
        {
            line.ToTable("PurchaseOrderLines");
            line.Property<Guid>("Id");
            line.HasKey("Id");
            line.Property(x => x.ItemName).HasMaxLength(300).IsRequired();
            line.Property(x => x.Quantity).IsRequired();
            line.OwnsOne(x => x.UnitPrice, money =>
            {
                money.Property(x => x.Amount).HasColumnName("UnitPriceAmount").HasPrecision(18, 2).IsRequired();
                money.Property(x => x.Currency).HasColumnName("UnitPriceCurrency").HasMaxLength(3).IsRequired();
            });
            line.Ignore(x => x.Total);
        });

        builder.Property(x => x.CreatedBy).HasMaxLength(100);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        builder.Property(x => x.DeletedBy).HasMaxLength(100);

        builder.HasIndex(x => x.VendorId);
        builder.HasIndex(x => x.PurchaseRequestId);
        builder.HasIndex(x => new { x.VendorId, x.Status });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
