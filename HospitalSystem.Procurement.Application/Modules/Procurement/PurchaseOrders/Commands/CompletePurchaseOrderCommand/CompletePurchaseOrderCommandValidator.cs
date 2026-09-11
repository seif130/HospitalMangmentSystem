using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CompletePurchaseOrderCommand
{
    public sealed class CompletePurchaseOrderCommandValidator : AbstractValidator<CompletePurchaseOrderCommand>
    {
        public CompletePurchaseOrderCommandValidator() => RuleFor(x => x.PurchaseOrderId.Value).NotEmpty();
    }

}
