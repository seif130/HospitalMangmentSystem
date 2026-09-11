using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetByIdQuery
{
    public sealed class GetBudgetByIdHandler(IBudgetRepository budgets) : IQueryHandler<GetBudgetByIdQuery, BudgetDto>
    {
        public async Task<Result<BudgetDto>> Handle(GetBudgetByIdQuery request, CancellationToken ct)
        {
            var budget = await budgets.GetByIdAsync(request.BudgetId, ct);
            return budget is null ? Result.Failure<BudgetDto>(
                Error.NotFound("Budget.NotFound", "Budget was not found.")) : Result.Success(budget.ToDto());
        }
    }

}
