using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.AddPurchaseOrderLineCommand
{
    public sealed class AddPurchaseOrderLineCommandValidator : AbstractValidator<AddPurchaseOrderLineCommand>
    {
        public AddPurchaseOrderLineCommandValidator()
        { 
            RuleFor(x => x.PurchaseOrderId.Value).NotEmpty();
            RuleFor(x => x.ItemName).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }

}
