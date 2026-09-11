using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorsQuery
{
    public sealed class GetVendorsQueryValidator : AbstractValidator<GetVendorsQuery>
    {
        public GetVendorsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

}
