using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery
{
    public sealed class GetBudgetsByDepartmentHandler(IBudgetRepository budgets) : IQueryHandler<GetBudgetsByDepartmentQuery, PaginatedList<BudgetDto>>
    {
        public async Task<Result<PaginatedList<BudgetDto>>> Handle(GetBudgetsByDepartmentQuery request, CancellationToken ct)
        {
            var (items, total) = await budgets.GetByDepartmentAsync(request.DepartmentId, request.PageNumber, request.PageSize, ct);
            return Result.Success(new PaginatedList<BudgetDto>(items.Select(x => x.ToDto()).ToList(), total, request.PageNumber, request.PageSize));
        }
    }

}
