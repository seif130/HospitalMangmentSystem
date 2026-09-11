using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.VendorContracts
{
    public sealed class VendorContractTerminatedHandler(
        ILogger<VendorContractTerminatedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<VendorContractTerminatedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<VendorContractTerminatedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Vendor contract {ContractId} terminated. Reason: {Reason}",
                notification.Event.VendorContractId.Value,
                notification.Event.Reason);

            return Task.CompletedTask;
        }
    }
}
