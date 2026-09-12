using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Domain.identfires;
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
