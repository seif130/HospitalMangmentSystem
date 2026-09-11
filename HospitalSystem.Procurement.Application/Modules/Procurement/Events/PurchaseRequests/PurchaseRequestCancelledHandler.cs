using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseRequests
{
    public sealed class PurchaseRequestCancelledHandler(
        ILogger<PurchaseRequestCancelledHandler> logger)
        : INotificationHandler<DomainEventNotification<PurchaseRequestCancelledDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseRequestCancelledDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Purchase request {RequestId} cancelled.",
                notification.Event.PurchaseRequestId.Value);

            return Task.CompletedTask;
        }
    }
}
