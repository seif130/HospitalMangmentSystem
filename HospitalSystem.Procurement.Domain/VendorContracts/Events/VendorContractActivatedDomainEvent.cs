using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;

public sealed record VendorContractActivatedDomainEvent(
    VendorContractId VendorContractId) : DomainEvent;
