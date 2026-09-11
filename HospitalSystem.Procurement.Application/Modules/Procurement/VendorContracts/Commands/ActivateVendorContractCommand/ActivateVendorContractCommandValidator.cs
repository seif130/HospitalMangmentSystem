using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ActivateVendorContractCommand
{
    public sealed class ActivateVendorContractCommandValidator : AbstractValidator<ActivateVendorContractCommand>
    {
        public ActivateVendorContractCommandValidator() => RuleFor(x => x.VendorContractId.Value).NotEmpty();
    }

}
