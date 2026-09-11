using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CancelPurchaseOrderCommand
{
    public sealed class CancelPurchaseOrderCommandValidator : AbstractValidator<CancelPurchaseOrderCommand>
    { 
        public CancelPurchaseOrderCommandValidator() => RuleFor(x => x.PurchaseOrderId.Value).NotEmpty();
    }

}
