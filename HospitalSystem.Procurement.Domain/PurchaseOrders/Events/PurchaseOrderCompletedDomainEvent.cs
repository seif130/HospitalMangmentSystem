using HospitalSystem.Domain.Primitives;
using HospitalSystem.Procurement.Domain.identfires;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;

public sealed record PurchaseOrderCompletedDomainEvent(
    PurchaseOrderId PurchaseOrderId) : DomainEvent;
