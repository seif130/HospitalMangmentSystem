using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Application.Abstractions.Persistence;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;

namespace HospitalSystem.Procurement.Application.Abstractions.Persistence;

public interface IPurchaseOrderRepository
    : IRepository<PurchaseOrder, PurchaseOrderId>
{
    Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByVendorAsync(
        VendorId vendorId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);

    Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByPurchaseRequestAsync(
        PurchaseRequestId purchaseRequestId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
