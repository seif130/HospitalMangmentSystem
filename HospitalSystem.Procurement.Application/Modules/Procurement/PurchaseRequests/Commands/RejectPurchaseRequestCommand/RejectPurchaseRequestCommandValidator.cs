using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand
{
    public sealed class RejectPurchaseRequestCommandValidator : AbstractValidator<RejectPurchaseRequestCommand>
    {
        public RejectPurchaseRequestCommandValidator()
        { 
            RuleFor(x => x.PurchaseRequestId.Value).NotEmpty();
            RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000);
        }
    }

}
