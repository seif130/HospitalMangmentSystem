using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand
{
    public sealed class RenameVendorCommandValidator : AbstractValidator<RenameVendorCommand>
    {
        public RenameVendorCommandValidator() 
        {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        }
    }

}
