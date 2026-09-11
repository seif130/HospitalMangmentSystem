using HospitalSystem.Domain.Identifiers;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Repositories;

internal sealed class PurchaseRequestRepository(ProcurementDbContext context) : Repository<PurchaseRequest, PurchaseRequestId>(context), IPurchaseRequestRepository
{
    public async Task<(IReadOnlyList<PurchaseRequest> Items, int TotalCount)> GetByDepartmentAsync(DepartmentId departmentId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        ValidatePaging(pageNumber, pageSize);

        var query = DbSet.AsNoTracking().Where(x => x.DepartmentId == departmentId).OrderByDescending(x => x.Id.Value);
        var total = await query.CountAsync(ct);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }


    private static void ValidatePaging(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber));

        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize));
    }
}
