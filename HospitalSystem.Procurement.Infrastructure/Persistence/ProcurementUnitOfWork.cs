using HospitalSystem.Application.Abstractions.Events;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;

namespace HospitalSystem.Procurement.Infrastructure.Persistence;

public sealed class ProcurementUnitOfWork(
    ProcurementDbContext context,
    IDomainEventDispatcher dispatcher) : IProcurementUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = context.ChangeTracker
            .Entries()
            .Select(entry => entry.Entity)
            .OfType<IHasDomainEvents>()
            .ToList();

        var events = entities
            .SelectMany(entity => entity.DomainEvents)
            .DistinctBy(domainEvent => domainEvent.EventId)
            .ToList();

        var result = await context.SaveChangesAsync(cancellationToken);

        if (events.Count > 0)
        {
            await dispatcher.DispatchAsync(events, cancellationToken);

            foreach (var entity in entities)
                entity.ClearDomainEvents();
        }

        return result;
    }
}
