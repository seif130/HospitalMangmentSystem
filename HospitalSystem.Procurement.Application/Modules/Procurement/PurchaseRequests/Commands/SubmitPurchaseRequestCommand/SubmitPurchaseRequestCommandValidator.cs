using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.SubmitPurchaseRequestCommand
{
    public sealed class SubmitPurchaseRequestCommandValidator : AbstractValidator<SubmitPurchaseRequestCommand>
    {
        public SubmitPurchaseRequestCommandValidator() => RuleFor(x => x.PurchaseRequestId.Value).NotEmpty(); 
    }

}
