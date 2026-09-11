using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.VendorContracts
{
    public sealed class VendorContractExpiredHandler(
        ILogger<VendorContractExpiredHandler> logger)
        : INotificationHandler<
            DomainEventNotification<VendorContractExpiredDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<VendorContractExpiredDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Vendor contract {ContractId} expired.",
                notification.Event.VendorContractId.Value);

            return Task.CompletedTask;
        }
    }
}
