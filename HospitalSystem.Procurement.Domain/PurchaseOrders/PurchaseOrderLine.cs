using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;

public sealed record PurchaseOrderLine
{
    public string ItemName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = null!;

    public Money Total
        => UnitPrice.Multiply(Quantity);

    // EF Core
    private PurchaseOrderLine()
    {
    }

    public PurchaseOrderLine(
        string itemName,
        int quantity,
        Money unitPrice)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            throw new DomainException("Item name is required.");

        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        ArgumentNullException.ThrowIfNull(unitPrice);

        ItemName = itemName.Trim();
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}