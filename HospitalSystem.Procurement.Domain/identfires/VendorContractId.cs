namespace HospitalSystem.Procurement.Domain.identfires;

public readonly record struct VendorContractId(Guid Value)
{
    public static VendorContractId New() => new(Guid.NewGuid());
    public bool IsEmpty => Value == Guid.Empty;
    public override string ToString() => Value.ToString();
}
