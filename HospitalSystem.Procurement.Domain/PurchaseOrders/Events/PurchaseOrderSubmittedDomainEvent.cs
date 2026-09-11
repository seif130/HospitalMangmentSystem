using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;

public sealed record PurchaseOrderSubmittedDomainEvent(
    PurchaseOrderId PurchaseOrderId) : DomainEvent;
