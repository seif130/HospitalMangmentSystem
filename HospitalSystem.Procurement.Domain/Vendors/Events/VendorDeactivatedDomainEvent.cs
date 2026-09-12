using HospitalSystem.Domain.Primitives;
using HospitalSystem.Procurement.Domain.identfires;

namespace HospitalSystem.Domain.Modules.Procurement.Vendors.Events;

public sealed record VendorDeactivatedDomainEvent(
    VendorId VendorId) : DomainEvent;
