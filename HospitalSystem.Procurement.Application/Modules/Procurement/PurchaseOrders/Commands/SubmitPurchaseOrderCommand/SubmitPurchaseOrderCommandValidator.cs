using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.SubmitPurchaseOrderCommand
{
    public sealed class SubmitPurchaseOrderCommandValidator : AbstractValidator<SubmitPurchaseOrderCommand>
    { 
        public SubmitPurchaseOrderCommandValidator() => RuleFor(x => x.PurchaseOrderId.Value).NotEmpty();
    }

}
