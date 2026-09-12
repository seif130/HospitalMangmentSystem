using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ExpireVendorContractCommand
{
    public sealed class ExpireVendorContractCommandValidator : AbstractValidator<ExpireVendorContractCommand>
    {
        public ExpireVendorContractCommandValidator() 
        {
            RuleFor(x => x.VendorContractId.Value).NotEmpty(); 
        }
    }

}
