using HospitalSystem.Procurement.Domain;
using HospitalSystem.Application.Abstractions.Persistence;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Procurement.Domain.identfires;

namespace HospitalSystem.Procurement.Application.Abstractions.Persistence;

public interface IBudgetRepository
    : IRepository<Budget, BudgetId>
{
    Task<(IReadOnlyList<Budget> Items, int TotalCount)> GetByDepartmentAsync(
        DepartmentId departmentId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);

    Task<bool> ExistsOverlappingAsync(
        DepartmentId departmentId,
        DateRange fiscalPeriod,
        CancellationToken ct = default);
}
