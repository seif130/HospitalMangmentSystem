using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestsByDepartmentQuery
{
    public sealed class GetPurchaseRequestsByDepartmentQueryValidator : AbstractValidator<GetPurchaseRequestsByDepartmentQuery>
    {
        public GetPurchaseRequestsByDepartmentQueryValidator() 
        { 
            RuleFor(x => x.DepartmentId.Value).NotEmpty(); 
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100); 
        }
    }

}
