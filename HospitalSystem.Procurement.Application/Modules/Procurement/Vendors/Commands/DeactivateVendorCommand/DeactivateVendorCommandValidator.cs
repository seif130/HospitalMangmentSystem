using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.DeactivateVendorCommand
{
    public sealed class DeactivateVendorCommandValidator : AbstractValidator<DeactivateVendorCommand>
    {
        public DeactivateVendorCommandValidator() => RuleFor(x => x.VendorId.Value).NotEmpty();
    }

}
