using HospitalSystem.Domain.Modules.Procurement.Budgets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Configurations;

internal sealed class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.BudgetId());
        builder.Property(x => x.DepartmentId).HasConversion(StrongIdValueConverters.DepartmentId()).IsRequired();

        builder.OwnsOne(x => x.FiscalPeriod, period =>
        {
            period.Property(x => x.Start).HasColumnName("FiscalStartUtc").IsRequired();
            period.Property(x => x.End).HasColumnName("FiscalEndUtc");
            period.Ignore(x => x.IsOpen);
        });

        builder.OwnsOne(x => x.AllocatedAmount, money =>
        {
            money.Property(x => x.Amount).HasColumnName("AllocatedAmount").HasPrecision(18, 2).IsRequired();
            money.Property(x => x.Currency).HasColumnName("AllocatedCurrency").HasMaxLength(3).IsRequired();
        });

        builder.OwnsMany(x => x.Expenses, expense =>
        {
            expense.ToTable("BudgetExpenses");
            expense.Property<Guid>("Id");
            expense.HasKey("Id");
            expense.Property(x => x.Description).HasMaxLength(500).IsRequired();
            expense.Property(x => x.IncurredOnUtc).IsRequired();
            expense.OwnsOne(x => x.Amount, money =>
            {
                money.Property(x => x.Amount).HasColumnName("Amount").HasPrecision(18, 2).IsRequired();
                money.Property(x => x.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
            });
        });

        builder.Property(x => x.CreatedBy).HasMaxLength(100);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        builder.Property(x => x.DeletedBy).HasMaxLength(100);

        builder.HasIndex(x => x.DepartmentId);
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
