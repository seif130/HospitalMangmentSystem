using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.Vendors
{
    public sealed class VendorActivatedHandler(
        ILogger<VendorActivatedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<VendorActivatedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<VendorActivatedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Vendor {VendorId} activated.",
                notification.Event.VendorId.Value);

            return Task.CompletedTask;
        }
    }
}
