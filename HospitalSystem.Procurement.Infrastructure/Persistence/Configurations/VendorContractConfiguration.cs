using HospitalSystem.Domain.Modules.Procurement.VendorContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Configurations;

internal sealed class VendorContractConfiguration : IEntityTypeConfiguration<VendorContract>
{
    public void Configure(EntityTypeBuilder<VendorContract> builder)
    {
        builder.ToTable("VendorContracts");
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.VendorContractId());

        builder.Property(x => x.VendorId).HasConversion(StrongIdValueConverters.VendorId()).IsRequired();
        builder.Property(x => x.Category).HasConversion<int>().IsRequired();
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();

        builder.OwnsOne(x => x.Term, term =>
        {
            term.Property(x => x.Start).HasColumnName("TermStartUtc").IsRequired();
            term.Property(x => x.End).HasColumnName("TermEndUtc");
            term.Ignore(x => x.IsOpen);
        });

        builder.OwnsOne(x => x.ContractValue, money =>
        {
            money.Property(x => x.Amount).HasColumnName("ContractValueAmount").HasPrecision(18, 2).IsRequired();
            money.Property(x => x.Currency).HasColumnName("ContractValueCurrency").HasMaxLength(3).IsRequired();
        });

        builder.Property(x => x.CreatedBy).HasMaxLength(100);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        builder.Property(x => x.DeletedBy).HasMaxLength(100);

        builder.HasIndex(x => x.VendorId);
        builder.HasIndex(x => new { x.VendorId, x.Status });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
