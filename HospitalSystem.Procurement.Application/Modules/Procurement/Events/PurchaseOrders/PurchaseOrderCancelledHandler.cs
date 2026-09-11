using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseOrders
{
    public sealed class PurchaseOrderCancelledHandler(
        ILogger<PurchaseOrderCancelledHandler> logger)
        : INotificationHandler<
            DomainEventNotification<PurchaseOrderCancelledDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseOrderCancelledDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Purchase order {OrderId} cancelled.",
                notification.Event.PurchaseOrderId.Value);

            return Task.CompletedTask;
        }
    }
}
