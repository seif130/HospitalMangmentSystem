using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseOrders
{
    public sealed class PurchaseOrderCompletedHandler(
        ILogger<PurchaseOrderCompletedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<PurchaseOrderCompletedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseOrderCompletedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Purchase order {OrderId} completed.",
                notification.Event.PurchaseOrderId.Value);

            return Task.CompletedTask;
        }
    }
}
