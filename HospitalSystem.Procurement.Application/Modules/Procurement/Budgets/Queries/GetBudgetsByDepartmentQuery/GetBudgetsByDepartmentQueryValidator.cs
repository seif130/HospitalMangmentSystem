using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery
{
    public sealed class GetBudgetsByDepartmentQueryValidator : AbstractValidator<GetBudgetsByDepartmentQuery>
    {
        public GetBudgetsByDepartmentQueryValidator()
        {
            RuleFor(x => x.DepartmentId.Value).NotEmpty();
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

}
