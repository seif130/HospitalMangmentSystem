using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.AllocateBudgetCommand
{
    public sealed class AllocateBudgetHandler(IBudgetRepository budgets, IProcurementUnitOfWork uow) : ICommandHandler<AllocateBudgetCommand, BudgetId>
    {
        public async Task<Result<BudgetId>> Handle(AllocateBudgetCommand request, CancellationToken ct)
        {
            var period = DateRange.Create(request.FiscalStart, request.FiscalEnd);
            if (await budgets.ExistsOverlappingAsync(request.DepartmentId, period, ct))
                return Result.Failure<BudgetId>(Error.Conflict("Budget.Overlap", "An overlapping budget already exists for this department."));
            var budget = Budget.Allocate(request.DepartmentId, period, Money.Create(request.Amount, request.Currency));
            await budgets.AddAsync(budget, ct); await uow.SaveChangesAsync(ct); return Result.Success(budget.Id);
        }
    }

}
