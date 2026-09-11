using HospitalSystem.Domain.Identifiers;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Repositories;

internal sealed class PurchaseOrderRepository(ProcurementDbContext context) : Repository<PurchaseOrder, PurchaseOrderId>(context), IPurchaseOrderRepository
{
    public async Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByVendorAsync(VendorId vendorId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        ValidatePaging(pageNumber, pageSize);

        var query = DbSet.AsNoTracking().Where(x => x.VendorId == vendorId).OrderByDescending(x => x.Id.Value);
        var total = await query.CountAsync(ct);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByPurchaseRequestAsync(PurchaseRequestId purchaseRequestId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        ValidatePaging(pageNumber, pageSize);

        var query = DbSet.AsNoTracking().Where(x => x.PurchaseRequestId == purchaseRequestId).OrderByDescending(x => x.Id.Value);
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
