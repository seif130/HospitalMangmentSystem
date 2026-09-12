using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;

public sealed record PurchaseRequestLine
{
    public string ItemName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public Money EstimatedUnitPrice { get; private set; } = null!;

    public Money EstimatedTotal
        => EstimatedUnitPrice.Multiply(Quantity);

    // EF Core
    private PurchaseRequestLine()
    {
    }

    public PurchaseRequestLine(
        string itemName,
        int quantity,
        Money estimatedUnitPrice)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            throw new DomainException("Item name is required.");

        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        ArgumentNullException.ThrowIfNull(estimatedUnitPrice);

        ItemName = itemName.Trim();
        Quantity = quantity;
        EstimatedUnitPrice = estimatedUnitPrice;
    }
}