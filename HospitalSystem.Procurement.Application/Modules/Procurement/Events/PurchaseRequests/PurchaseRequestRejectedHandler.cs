using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseRequests
{
    public sealed class PurchaseRequestRejectedHandler(
        ILogger<PurchaseRequestRejectedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<PurchaseRequestRejectedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseRequestRejectedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogWarning(
                "Purchase request {RequestId} rejected. Reason: {Reason}",
                notification.Event.PurchaseRequestId.Value,
                notification.Event.Reason);

            return Task.CompletedTask;
        }
    }
}
