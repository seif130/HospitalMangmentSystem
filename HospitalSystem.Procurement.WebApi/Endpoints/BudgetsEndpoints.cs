using HospitalSystem.Application.Models;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.AllocateBudgetCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HospitalSystem.Procurement.WebApi.Endpoints;

public static class BudgetsEndpoints
{
    public static void MapBudgetsEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/budgets")
            .WithTags("Budgets");

        MapAllocateBudget(group);
        MapGetBudgetById(group);
        MapGetBudgetsByDepartment(group);
        MapRecordBudgetExpense(group);
    }

    private static void MapAllocateBudget(
        RouteGroupBuilder group)
    {
        group.MapPost("", async (
            AllocateBudgetRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AllocateBudgetCommand(
                new DepartmentId(request.DepartmentId),
                request.FiscalStart,
                request.FiscalEnd,
                request.Amount,
                request.Currency);

            return await EndpointHelper.SendAsync<
                AllocateBudgetCommand,
                BudgetId>(
                command,
                sender,
                cancellationToken,
                id => Results.Created(
                    $"/api/procurement/budgets/{id.Value}",
                    id.Value));
        });
    }

    private static void MapGetBudgetById(
        RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetBudgetByIdQuery(
                new BudgetId(id));

            return await EndpointHelper.SendAsync<
                GetBudgetByIdQuery,
                BudgetDto>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapGetBudgetsByDepartment(
        RouteGroupBuilder group)
    {
        group.MapGet("/department/{departmentId:guid}", async (
            Guid departmentId,
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 20 : pageSize;

            var query = new GetBudgetsByDepartmentQuery(
                new DepartmentId(departmentId),
                pageNumber,
                pageSize);

            return await EndpointHelper.SendAsync<
                GetBudgetsByDepartmentQuery,
                PaginatedList<BudgetDto>>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapRecordBudgetExpense(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/expenses", async (
            Guid id,
            RecordExpenseRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RecordBudgetExpenseCommand(
                new BudgetId(id),
                request.Description,
                request.Amount,
                request.Currency,
                request.IncurredOnUtc);

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    public sealed record AllocateBudgetRequest(
        Guid DepartmentId,
        DateTime FiscalStart,
        DateTime FiscalEnd,
        decimal Amount,
        string Currency = "USD");

    public sealed record RecordExpenseRequest(
        string Description,
        decimal Amount,
        string Currency,
        DateTime IncurredOnUtc);
}