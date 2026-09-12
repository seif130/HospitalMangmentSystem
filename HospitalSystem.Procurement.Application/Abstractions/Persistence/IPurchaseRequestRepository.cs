using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Application.Abstractions.Persistence;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;

namespace HospitalSystem.Procurement.Application.Abstractions.Persistence;

public interface IPurchaseRequestRepository
    : IRepository<PurchaseRequest, PurchaseRequestId>
{
    Task<(IReadOnlyList<PurchaseRequest> Items, int TotalCount)> GetByDepartmentAsync(
        DepartmentId departmentId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
