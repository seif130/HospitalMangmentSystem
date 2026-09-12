using HospitalSystem.Domain.Primitives;
namespace HospitalSystem.Domain.ValueObjects;
public sealed class PersonName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    private PersonName(string firstName, string lastName) { FirstName = firstName; LastName = lastName; }
    public static PersonName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new DomainException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new DomainException("Last name is required.");
        return new(firstName.Trim(), lastName.Trim());
    }
    protected override IEnumerable<object?> GetEqualityComponents() { yield return FirstName; yield return LastName; }
    public override string ToString() => $"{FirstName} {LastName}";
}
