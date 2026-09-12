using HospitalSystem.Domain.Modules.Procurement.Vendors;
using HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Repositories;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Domain.identfires;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Repositories;

internal sealed class VendorRepository(
    ProcurementDbContext context)
    : Repository<Vendor, VendorId>(context), IVendorRepository
{
    public Task<bool> ExistsByNormalizedNameAsync(
        string normalizedName,
        VendorId? excludingId = null,
        CancellationToken ct = default)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(x => x.NormalizedName == normalizedName);

        if (excludingId.HasValue)
        {
            query = query.Where(x => x.Id != excludingId.Value);
        }

        return query.AnyAsync(ct);
    }

    public async Task<(
        IReadOnlyList<Vendor> Items,
        int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        var query = DbSet
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Id.Value);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }
}