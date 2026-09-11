using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand
{
    public sealed class RecordBudgetExpenseCommandValidator : AbstractValidator<RecordBudgetExpenseCommand>
    {
        public RecordBudgetExpenseCommandValidator() 
        { 
            RuleFor(x => x.BudgetId.Value).NotEmpty(); 
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500); 
            RuleFor(x => x.Amount).GreaterThan(0); RuleFor(x => x.Currency).NotEmpty().Length(3); 
            RuleFor(x => x.IncurredOnUtc).NotEqual(default(DateTime)); 
        }
    }

}
