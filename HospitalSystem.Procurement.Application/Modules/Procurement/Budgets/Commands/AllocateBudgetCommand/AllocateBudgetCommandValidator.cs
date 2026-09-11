using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.AllocateBudgetCommand
{
    public sealed class AllocateBudgetCommandValidator : AbstractValidator<AllocateBudgetCommand>
    {
        public AllocateBudgetCommandValidator()
        { 
            RuleFor(x => x.DepartmentId.Value).NotEmpty();
            RuleFor(x => x.FiscalEnd).GreaterThan(x => x.FiscalStart);
            RuleFor(x => x.Amount).GreaterThan(0); RuleFor(x => x.Currency).NotEmpty().Length(3); 
        }
    }

}
