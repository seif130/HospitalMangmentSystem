using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.Identifiers;

public sealed record DepartmentId(Guid Value) : TypedId(Value)
{
    public static DepartmentId New() => new(Guid.NewGuid());
    public bool IsEmpty => Value == Guid.Empty;
}

public sealed record DoctorId(Guid Value) : TypedId(Value)
{
    public static DoctorId New() => new(Guid.NewGuid());
    public bool IsEmpty => Value == Guid.Empty;
}

public sealed record PatientId(Guid Value) : TypedId(Value)
{
    public bool IsEmpty => Value == Guid.Empty;
}

public sealed record VendorId(Guid Value) : TypedId(Value)
{
    public static VendorId New() => new(Guid.NewGuid());
    public bool IsEmpty => Value == Guid.Empty;
}
