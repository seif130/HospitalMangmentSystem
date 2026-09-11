using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.PurchaseOrders
{
    public sealed class PurchaseOrderApprovedHandler(
        ILogger<PurchaseOrderApprovedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<PurchaseOrderApprovedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<PurchaseOrderApprovedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Purchase order {OrderId} approved.",
                notification.Event.PurchaseOrderId.Value);

            return Task.CompletedTask;
        }
    }
}
