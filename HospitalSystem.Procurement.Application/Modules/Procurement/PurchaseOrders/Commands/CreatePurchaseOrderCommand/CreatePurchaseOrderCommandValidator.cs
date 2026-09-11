using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand
{
    public sealed class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
    {
        public CreatePurchaseOrderCommandValidator()
        { 
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }

}
