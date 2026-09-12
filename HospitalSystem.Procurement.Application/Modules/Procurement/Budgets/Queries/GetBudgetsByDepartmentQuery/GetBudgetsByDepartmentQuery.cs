using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Models;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery
{
    public sealed record GetBudgetsByDepartmentQuery(DepartmentId DepartmentId, int PageNumber = 1,
        int PageSize = 20) : IQuery<PaginatedList<BudgetDto>>;

}
