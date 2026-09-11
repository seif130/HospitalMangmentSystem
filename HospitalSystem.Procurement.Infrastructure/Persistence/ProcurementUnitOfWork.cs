using System.Text.Json;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Procurement.Infrastructure.Outbox;

namespace HospitalSystem.Procurement.Infrastructure.Persistence;

public sealed class ProcurementUnitOfWork(ProcurementDbContext context) : IUnitOfWork
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var eventEntries = context.ChangeTracker
            .Entries()
            .Select(entry => entry.Entity)
            .OfType<IHasDomainEvents>()
            .ToList();

        var domainEvents = eventEntries
            .SelectMany(entity => entity.DomainEvents)
            .DistinctBy(domainEvent => domainEvent.EventId)
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            var type = domainEvent.GetType();
            var typeName = type.AssemblyQualifiedName ?? type.FullName ?? type.Name;
            var payload = JsonSerializer.Serialize(domainEvent, type, SerializerOptions);

            await context.OutboxMessages.AddAsync(
                new OutboxMessage(
                    domainEvent.EventId,
                    typeName,
                    payload,
                    domainEvent.OccurredOnUtc),
                ct);
        }

        var result = await context.SaveChangesAsync(ct);

        foreach (var entity in eventEntries)
            entity.ClearDomainEvents();

        return result;
    }
}
