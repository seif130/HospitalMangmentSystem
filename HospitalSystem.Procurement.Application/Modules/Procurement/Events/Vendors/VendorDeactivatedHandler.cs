using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.Vendors
{
    public sealed class VendorDeactivatedHandler(
        ILogger<VendorDeactivatedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<VendorDeactivatedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<VendorDeactivatedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Vendor {VendorId} deactivated.",
                notification.Event.VendorId.Value);

            return Task.CompletedTask;
        }
    }
}
