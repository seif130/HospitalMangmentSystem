using HospitalSystem.Domain.Primitives;
using MediatR;

namespace HospitalSystem.Application.Modules.Procurement.Events;

public sealed record DomainEventNotification<TEvent>(TEvent Event) : INotification where TEvent : IDomainEvent;
