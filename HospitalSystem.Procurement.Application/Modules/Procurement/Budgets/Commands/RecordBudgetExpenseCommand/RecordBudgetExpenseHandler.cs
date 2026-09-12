using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand
{
    public sealed class RecordBudgetExpenseHandler(IBudgetRepository budgets, IProcurementUnitOfWork uow) : ICommandHandler<RecordBudgetExpenseCommand>
    {
        public async Task<Result> Handle(RecordBudgetExpenseCommand request, CancellationToken ct)
        {
            var budget = await budgets.GetByIdAsync(request.BudgetId, ct);
            if (budget is null) return Result.Failure(Error.NotFound("Budget.NotFound", "Budget was not found."));
            budget.RecordExpense(request.Description, Money.Create(request.Amount, request.Currency), request.IncurredOnUtc);
            await uow.SaveChangesAsync(ct); return Result.Success();
        }
    }

}
