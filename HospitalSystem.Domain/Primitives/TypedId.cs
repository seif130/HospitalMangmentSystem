namespace HospitalSystem.Domain.Primitives;

public abstract record TypedId(Guid Value)
{
    public override string ToString() => Value.ToString();
}
