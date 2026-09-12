using HospitalSystem.Application.Abstractions.Events;
using HospitalSystem.Domain.Primitives;
using MediatR;
namespace HospitalSystem.Application.Common.Events;
public sealed class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            var notification = Activator.CreateInstance(notificationType, domainEvent)
                ?? throw new InvalidOperationException($"Could not create notification for {domainEvent.GetType().Name}.");
            await mediator.Publish((INotification)notification, ct);
        }
    }
}
