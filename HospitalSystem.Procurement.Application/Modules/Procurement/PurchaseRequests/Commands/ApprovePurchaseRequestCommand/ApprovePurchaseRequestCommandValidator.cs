using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.ApprovePurchaseRequestCommand
{
    public sealed class ApprovePurchaseRequestCommandValidator : AbstractValidator<ApprovePurchaseRequestCommand>
    { 
        public ApprovePurchaseRequestCommandValidator() => RuleFor(x => x.PurchaseRequestId.Value).NotEmpty();
    }

}
