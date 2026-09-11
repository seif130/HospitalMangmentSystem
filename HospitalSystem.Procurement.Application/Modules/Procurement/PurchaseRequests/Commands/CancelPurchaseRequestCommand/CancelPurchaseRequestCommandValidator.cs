using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.CancelPurchaseRequestCommand
{
    public sealed class CancelPurchaseRequestCommandValidator : AbstractValidator<CancelPurchaseRequestCommand>
    { 
        public CancelPurchaseRequestCommandValidator() => RuleFor(x => x.PurchaseRequestId.Value).NotEmpty();
    }

}
