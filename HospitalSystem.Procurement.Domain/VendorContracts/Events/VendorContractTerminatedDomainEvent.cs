using HospitalSystem.Domain.Primitives;
using HospitalSystem.Procurement.Domain.identfires;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;

public sealed record VendorContractTerminatedDomainEvent(
    VendorContractId VendorContractId,
    string Reason) : DomainEvent;
