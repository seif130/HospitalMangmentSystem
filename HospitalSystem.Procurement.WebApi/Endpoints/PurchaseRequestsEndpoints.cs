using MediatR;
using HospitalSystem.WebApi.Common;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.CreatePurchaseRequestCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.CancelPurchaseRequestCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.ApprovePurchaseRequestCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.SubmitPurchaseRequestCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.AddPurchaseRequestLineCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestsByDepartmentQuery;
using HospitalSystem.Application.Models;

namespace HospitalSystem.Procurement.WebApi.Endpoints;

public static class PurchaseRequestsEndpoints
{
    public static void MapPurchaseRequestsEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/purchase-requests")
            .WithTags("Purchase Requests");

        MapCreatePurchaseRequest(group);
        MapGetPurchaseRequestById(group);
        MapGetPurchaseRequestsByDepartment(group);
        MapAddPurchaseRequestLine(group);
        MapSubmitPurchaseRequest(group);
        MapApprovePurchaseRequest(group);
        MapRejectPurchaseRequest(group);
        MapCancelPurchaseRequest(group);
    }

    private static void MapCreatePurchaseRequest(
        RouteGroupBuilder group)
    {
        group.MapPost("", async (
            CreatePurchaseRequestRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreatePurchaseRequestCommand(
                new DepartmentId(request.DepartmentId),
                request.Reason);

            return await EndpointHelper.SendAsync<
                CreatePurchaseRequestCommand,
                PurchaseRequestId>(
                command,
                sender,
                cancellationToken,
                id => Results.Created(
                    $"/api/procurement/purchase-requests/{id.Value}",
                    id.Value));
        });
    }

    private static void MapGetPurchaseRequestById(
        RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPurchaseRequestByIdQuery(
                new PurchaseRequestId(id));

            return await EndpointHelper.SendAsync<
                GetPurchaseRequestByIdQuery,
                PurchaseRequestDto>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapGetPurchaseRequestsByDepartment(
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

            var query = new GetPurchaseRequestsByDepartmentQuery(
                new DepartmentId(departmentId),
                pageNumber,
                pageSize);

            return await EndpointHelper.SendAsync<
                GetPurchaseRequestsByDepartmentQuery,
                PaginatedList<PurchaseRequestDto>>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapAddPurchaseRequestLine(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/lines", async (
            Guid id,
            RequestLine request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AddPurchaseRequestLineCommand(
                new PurchaseRequestId(id),
                request.ItemName,
                request.Quantity,
                request.UnitPrice,
                request.Currency);

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapSubmitPurchaseRequest(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/submit", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new SubmitPurchaseRequestCommand(
                new PurchaseRequestId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapApprovePurchaseRequest(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/approve", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ApprovePurchaseRequestCommand(
                new PurchaseRequestId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapRejectPurchaseRequest(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/reject", async (
            Guid id,
            RejectRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RejectPurchaseRequestCommand(
                new PurchaseRequestId(id),
                request.Reason);

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapCancelPurchaseRequest(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/cancel", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CancelPurchaseRequestCommand(
                new PurchaseRequestId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    public sealed record CreatePurchaseRequestRequest(
        Guid DepartmentId,
        string Reason);

    public sealed record RequestLine(
        string ItemName,
        int Quantity,
        decimal UnitPrice,
        string Currency = "USD");

    public sealed record RejectRequest(
        string Reason);
}