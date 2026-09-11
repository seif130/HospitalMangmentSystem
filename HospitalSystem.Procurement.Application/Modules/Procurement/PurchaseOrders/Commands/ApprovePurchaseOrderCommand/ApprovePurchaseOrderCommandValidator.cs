using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.ApprovePurchaseOrderCommand
{
    public sealed class ApprovePurchaseOrderCommandValidator : AbstractValidator<ApprovePurchaseOrderCommand>
    { 
        public ApprovePurchaseOrderCommandValidator() => RuleFor(x => x.PurchaseOrderId.Value).NotEmpty();
    }

}
