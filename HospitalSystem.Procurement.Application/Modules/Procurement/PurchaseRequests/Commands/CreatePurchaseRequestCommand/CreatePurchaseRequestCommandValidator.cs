using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.CreatePurchaseRequestCommand
{
    public sealed class CreatePurchaseRequestCommandValidator : AbstractValidator<CreatePurchaseRequestCommand>
    {
        public CreatePurchaseRequestCommandValidator() 
        {
            RuleFor(x => x.DepartmentId.Value).NotEmpty(); 
            RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000); 
        }
    }

}
