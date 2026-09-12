using HospitalSystem.Application.Abstractions.Time;
namespace HospitalSystem.Infrastructure.Common.Time;
public sealed class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
