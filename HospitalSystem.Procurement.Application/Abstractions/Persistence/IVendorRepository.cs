using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Application.Abstractions.Persistence;
using HospitalSystem.Domain.Modules.Procurement.Vendors;

namespace HospitalSystem.Procurement.Application.Abstractions.Persistence;

public interface IVendorRepository
    : IRepository<Vendor, VendorId>
{
    Task<bool> ExistsByNormalizedNameAsync(
        string normalizedName,
        VendorId? excludingId = null,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<Vendor> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
