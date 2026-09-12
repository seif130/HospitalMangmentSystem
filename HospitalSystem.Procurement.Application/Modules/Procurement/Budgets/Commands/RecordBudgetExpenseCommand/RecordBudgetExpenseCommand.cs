using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand
{
    public sealed record RecordBudgetExpenseCommand(BudgetId BudgetId, string Description,
        decimal Amount, string Currency, DateTime IncurredOnUtc) : ICommand;

}
