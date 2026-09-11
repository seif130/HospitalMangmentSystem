using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.VendorContracts
{
    public sealed class VendorContractActivatedHandler(
        ILogger<VendorContractActivatedHandler> logger)
        : INotificationHandler<
            DomainEventNotification<VendorContractActivatedDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<VendorContractActivatedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Vendor contract {ContractId} activated.",
                notification.Event.VendorContractId.Value);

            return Task.CompletedTask;
        }
    }
}
