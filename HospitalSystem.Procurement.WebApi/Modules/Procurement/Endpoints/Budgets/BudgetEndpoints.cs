using HospitalSystem.Application.Models;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.AllocateBudgetCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.WebApi.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static HospitalSystem.WebApi.Modules.Procurement.Endpoints.ProcurementEndpoints;

namespace HospitalSystem.Procurement.WebApi.Modules.Procurement.Endpoints.Budgets
{
    public static class BudgetEndpoints
    {
        public static RouteGroupBuilder MapBudgetEndpoints(
            this RouteGroupBuilder group)
        {
            var budgets = group
                .MapGroup("/budgets")
                .WithTags("Budgets");

            budgets.MapPost("", Create);
            budgets.MapGet("/{id:guid}", GetById);
            budgets.MapGet("/department/{departmentId:guid}", GetByDepartment);
            budgets.MapPost("/{id:guid}/expenses", RecordExpense);

            return budgets;
        }

        private static async Task<IResult> Create(
            AllocateBudgetRequest body,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new AllocateBudgetCommand(
                    new DepartmentId(body.DepartmentId),
                    body.FiscalStart,
                    body.FiscalEnd,
                    body.Amount,
                    body.Currency),
                sender,
                ct,
                id => Results.Created(
                    $"/api/procurement/budgets/{id.Value}",
                    id.Value));
        }

        private static async Task<IResult> GetById(
            Guid id,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync<
                GetBudgetByIdQuery,
                BudgetDto>(
                new(new BudgetId(id)),
                sender,
                ct);
        }

        private static async Task<IResult> GetByDepartment(
            Guid departmentId,
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync<
                GetBudgetsByDepartmentQuery,
                PaginatedList<BudgetDto>>(
                new(
                    new DepartmentId(departmentId),
                    pageNumber == 0 ? 1 : pageNumber,
                    pageSize == 0 ? 20 : pageSize),
                sender,
                ct);
        }

        private static async Task<IResult> RecordExpense(
            Guid id,
            RecordExpenseRequest body,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new RecordBudgetExpenseCommand(
                    new BudgetId(id),
                    body.Description,
                    body.Amount,
                    body.Currency,
                    body.IncurredOnUtc),
                sender,
                ct);
        }
    }
}
