using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.TerminateVendorContractCommand
{
    public sealed class TerminateVendorContractCommandValidator : AbstractValidator<TerminateVendorContractCommand>
    {
        public TerminateVendorContractCommandValidator() 
        { 
            RuleFor(x => x.VendorContractId.Value).NotEmpty(); 

            RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000);
        }
    }

}
