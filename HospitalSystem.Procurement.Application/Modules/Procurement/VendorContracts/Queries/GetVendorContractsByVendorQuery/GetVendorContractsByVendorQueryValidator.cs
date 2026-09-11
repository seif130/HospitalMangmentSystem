using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractsByVendorQuery
{
    public sealed class GetVendorContractsByVendorQueryValidator : AbstractValidator<GetVendorContractsByVendorQuery>
    {
        public GetVendorContractsByVendorQueryValidator() 
        { 
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.PageNumber).GreaterThan(0); 
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

}
