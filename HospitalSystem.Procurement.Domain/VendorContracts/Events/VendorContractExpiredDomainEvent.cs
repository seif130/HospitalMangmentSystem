using HospitalSystem.Domain.Primitives;
using HospitalSystem.Procurement.Domain.identfires;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;

public sealed record VendorContractExpiredDomainEvent(
    VendorContractId VendorContractId) : DomainEvent;
