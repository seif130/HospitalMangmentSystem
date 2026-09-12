using HospitalSystem.Domain.Primitives;
using HospitalSystem.Procurement.Domain.identfires;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;

public sealed record PurchaseRequestSubmittedDomainEvent(
    PurchaseRequestId PurchaseRequestId) : DomainEvent;
