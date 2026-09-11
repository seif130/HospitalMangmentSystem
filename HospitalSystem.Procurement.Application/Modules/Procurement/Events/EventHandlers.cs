using HospitalSystem.Domain.Modules.Procurement.Budgets.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalSystem.Application.Modules.Procurement.Events;


public sealed class VendorActivatedHandler(ILogger<VendorActivatedHandler> logger) : INotificationHandler<DomainEventNotification<VendorActivatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<VendorActivatedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Vendor {VendorId} activated.", n.Event.VendorId.Value); return Task.CompletedTask; }
}
public sealed class VendorDeactivatedHandler(ILogger<VendorDeactivatedHandler> logger) : INotificationHandler<DomainEventNotification<VendorDeactivatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<VendorDeactivatedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Vendor {VendorId} deactivated.", n.Event.VendorId.Value); return Task.CompletedTask; }
}

public sealed class VendorContractActivatedHandler(ILogger<VendorContractActivatedHandler> logger) : INotificationHandler<DomainEventNotification<VendorContractActivatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<VendorContractActivatedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Vendor contract {ContractId} activated.", n.Event.VendorContractId.Value); return Task.CompletedTask; }
}
public sealed class VendorContractExpiredHandler(ILogger<VendorContractExpiredHandler> logger) : INotificationHandler<DomainEventNotification<VendorContractExpiredDomainEvent>>
{
    public Task Handle(DomainEventNotification<VendorContractExpiredDomainEvent> n, CancellationToken ct) { logger.LogInformation("Vendor contract {ContractId} expired.", n.Event.VendorContractId.Value); return Task.CompletedTask; }
}
public sealed class VendorContractTerminatedHandler(ILogger<VendorContractTerminatedHandler> logger) : INotificationHandler<DomainEventNotification<VendorContractTerminatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<VendorContractTerminatedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Vendor contract {ContractId} terminated. Reason: {Reason}", n.Event.VendorContractId.Value, n.Event.Reason); return Task.CompletedTask; }
}

public sealed class PurchaseRequestSubmittedHandler(ILogger<PurchaseRequestSubmittedHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseRequestSubmittedDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseRequestSubmittedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Purchase request {RequestId} submitted.", n.Event.PurchaseRequestId.Value); return Task.CompletedTask; }
}
public sealed class PurchaseRequestApprovedHandler(ILogger<PurchaseRequestApprovedHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseRequestApprovedDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseRequestApprovedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Purchase request {RequestId} approved.", n.Event.PurchaseRequestId.Value); return Task.CompletedTask; }
}
public sealed class PurchaseRequestRejectedHandler(ILogger<PurchaseRequestRejectedHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseRequestRejectedDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseRequestRejectedDomainEvent> n, CancellationToken ct) { logger.LogWarning("Purchase request {RequestId} rejected. Reason: {Reason}", n.Event.PurchaseRequestId.Value, n.Event.Reason); return Task.CompletedTask; }
}
public sealed class PurchaseRequestCancelledHandler(ILogger<PurchaseRequestCancelledHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseRequestCancelledDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseRequestCancelledDomainEvent> n, CancellationToken ct) { logger.LogInformation("Purchase request {RequestId} cancelled.", n.Event.PurchaseRequestId.Value); return Task.CompletedTask; }
}

public sealed class PurchaseOrderSubmittedHandler(ILogger<PurchaseOrderSubmittedHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseOrderSubmittedDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseOrderSubmittedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Purchase order {OrderId} submitted.", n.Event.PurchaseOrderId.Value); return Task.CompletedTask; }
}
public sealed class PurchaseOrderApprovedHandler(ILogger<PurchaseOrderApprovedHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseOrderApprovedDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseOrderApprovedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Purchase order {OrderId} approved.", n.Event.PurchaseOrderId.Value); return Task.CompletedTask; }
}
public sealed class PurchaseOrderCancelledHandler(ILogger<PurchaseOrderCancelledHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseOrderCancelledDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseOrderCancelledDomainEvent> n, CancellationToken ct) { logger.LogInformation("Purchase order {OrderId} cancelled.", n.Event.PurchaseOrderId.Value); return Task.CompletedTask; }
}
public sealed class PurchaseOrderCompletedHandler(ILogger<PurchaseOrderCompletedHandler> logger) : INotificationHandler<DomainEventNotification<PurchaseOrderCompletedDomainEvent>>
{
    public Task Handle(DomainEventNotification<PurchaseOrderCompletedDomainEvent> n, CancellationToken ct) { logger.LogInformation("Purchase order {OrderId} completed.", n.Event.PurchaseOrderId.Value); return Task.CompletedTask; }
}
