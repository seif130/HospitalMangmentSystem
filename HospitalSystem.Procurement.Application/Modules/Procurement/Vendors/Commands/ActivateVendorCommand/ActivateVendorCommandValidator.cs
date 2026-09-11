using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.ActivateVendorCommand
{
    public sealed class ActivateVendorCommandValidator : AbstractValidator<ActivateVendorCommand>
    { 
        public ActivateVendorCommandValidator() => RuleFor(x => x.VendorId.Value).NotEmpty();
    }

}
