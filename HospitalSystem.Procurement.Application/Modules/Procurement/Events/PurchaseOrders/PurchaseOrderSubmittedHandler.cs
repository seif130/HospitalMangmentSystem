using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseOrders
{
    public sealed class PurchaseOrderSubmittedHandler(
        ILogger<PurchaseOrderSubmittedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<PurchaseOrderSubmittedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseOrderSubmittedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Purchase order {OrderId} submitted.",
                notification.Event.PurchaseOrderId.Value);

            return Task.CompletedTask;
        }
    }
}
