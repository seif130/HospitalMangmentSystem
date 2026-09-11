using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseRequests
{
    public sealed class PurchaseRequestApprovedHandler(
        ILogger<PurchaseRequestApprovedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<PurchaseRequestApprovedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseRequestApprovedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Purchase request {RequestId} approved.",
                notification.Event.PurchaseRequestId.Value);

            return Task.CompletedTask;
        }
    }
}
