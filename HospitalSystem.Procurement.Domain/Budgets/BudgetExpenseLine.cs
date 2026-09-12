using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.Budgets;

public sealed record BudgetExpenseLine
{
    public string Description { get; private set; } = null!;
    public Money Amount { get; private set; } = null!;
    public DateTime IncurredOnUtc { get; private set; }

    // EF Core
    private BudgetExpenseLine()
    {
    }

    public BudgetExpenseLine(
        string description,
        Money amount,
        DateTime incurredOnUtc)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                "Expense description is required.");

        ArgumentNullException.ThrowIfNull(amount);

        if (amount.Amount <= 0)
            throw new DomainException(
                "Expense amount must be greater than zero.");

        if (incurredOnUtc.Kind == DateTimeKind.Local)
            throw new DomainException(
                "Expense date must use UTC or unspecified DateTime values.");

        Description = description.Trim();
        Amount = amount;
        IncurredOnUtc = incurredOnUtc;
    }
}