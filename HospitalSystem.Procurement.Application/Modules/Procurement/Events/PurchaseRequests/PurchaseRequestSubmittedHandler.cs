using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseRequests
{
    public sealed class PurchaseRequestSubmittedHandler(
        ILogger<PurchaseRequestSubmittedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<PurchaseRequestSubmittedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseRequestSubmittedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Purchase request {RequestId} submitted.",
                notification.Event.PurchaseRequestId.Value);

            return Task.CompletedTask;
        }
    }
}
