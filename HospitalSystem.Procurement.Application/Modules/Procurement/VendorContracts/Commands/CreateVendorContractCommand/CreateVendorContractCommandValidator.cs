using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand
{
    public sealed class CreateVendorContractCommandValidator : AbstractValidator<CreateVendorContractCommand>
    {
        public CreateVendorContractCommandValidator() 
        {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.StartUtc).NotEqual(default(DateTime));
            RuleFor(x => x.EndUtc).GreaterThan(x => x.StartUtc);
            RuleFor(x => x.Amount).GreaterThan(0); RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }

}
