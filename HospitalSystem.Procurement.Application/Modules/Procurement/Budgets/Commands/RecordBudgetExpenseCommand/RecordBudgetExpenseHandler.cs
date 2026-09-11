using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Domain.ValueObjects;
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
