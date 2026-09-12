using HospitalSystem.Procurement.Domain;
using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Application.Abstractions.Persistence;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts;

namespace HospitalSystem.Procurement.Application.Abstractions.Persistence;

public interface IVendorContractRepository
    : IRepository<VendorContract, VendorContractId>
{
    Task<(IReadOnlyList<VendorContract> Items, int TotalCount)> GetByVendorAsync(
        VendorId vendorId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
