using HospitalSystem.Domain.Primitives;
namespace HospitalSystem.Application.Abstractions.Events;
public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
}
