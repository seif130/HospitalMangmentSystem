using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.Modules.Procurement.Vendors.Events;

public sealed record VendorActivatedDomainEvent(
    VendorId VendorId) : DomainEvent;
