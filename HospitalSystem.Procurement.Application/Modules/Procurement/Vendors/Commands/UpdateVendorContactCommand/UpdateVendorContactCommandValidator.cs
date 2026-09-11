using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand
{
    public sealed class UpdateVendorContactCommandValidator : AbstractValidator<UpdateVendorContactCommand>
    {
        public UpdateVendorContactCommandValidator() 
        {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.ContactEmail).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));
        }
    }

}
