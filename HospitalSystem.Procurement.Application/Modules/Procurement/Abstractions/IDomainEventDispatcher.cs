using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Application.Modules.Procurement.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
}
