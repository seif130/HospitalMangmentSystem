using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Repositories;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Repositories;

internal sealed class BudgetRepository(
    ProcurementDbContext context)
    : Repository<Budget, BudgetId>(context), IBudgetRepository
{
    public async Task<(IReadOnlyList<Budget> Items, int TotalCount)> GetByDepartmentAsync(
        DepartmentId departmentId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(x => x.DepartmentId == departmentId)
            .OrderByDescending(x => x.FiscalPeriod.Start)
            .ThenByDescending(x => x.Id.Value);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    public Task<bool> ExistsOverlappingAsync(
        DepartmentId departmentId,
        DateRange fiscalPeriod,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(fiscalPeriod);

        if (fiscalPeriod.IsOpen)
        {
            throw new ArgumentException(
                "Budget fiscal period must have an end date.",
                nameof(fiscalPeriod));
        }

        return DbSet
            .AsNoTracking()
            .AnyAsync(
                x =>
                    x.DepartmentId == departmentId &&
                    x.FiscalPeriod.Start < fiscalPeriod.End!.Value &&
                    fiscalPeriod.Start < x.FiscalPeriod.End!.Value,
                ct);
    }
}