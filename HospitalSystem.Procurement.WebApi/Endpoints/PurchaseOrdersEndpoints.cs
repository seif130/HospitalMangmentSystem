using MediatR;
using HospitalSystem.WebApi.Common;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand;
using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByVendorQuery;
using HospitalSystem.Application.Models;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrderByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByPurchaseRequestQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.AddPurchaseOrderLineCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.SubmitPurchaseOrderCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.ApprovePurchaseOrderCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CancelPurchaseOrderCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CompletePurchaseOrderCommand;

namespace HospitalSystem.Procurement.WebApi.Endpoints;

public static class PurchaseOrdersEndpoints
{
    public static void MapPurchaseOrdersEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/purchase-orders")
            .WithTags("Purchase Orders");

        MapCreatePurchaseOrder(group);
        MapGetPurchaseOrderById(group);
        MapGetPurchaseOrdersByVendor(group);
        MapGetPurchaseOrdersByPurchaseRequest(group);
        MapAddPurchaseOrderLine(group);
        MapSubmitPurchaseOrder(group);
        MapApprovePurchaseOrder(group);
        MapCancelPurchaseOrder(group);
        MapCompletePurchaseOrder(group);
    }

    private static void MapCreatePurchaseOrder(
        RouteGroupBuilder group)
    {
        group.MapPost("", async (
            CreatePurchaseOrderRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            PurchaseRequestId? purchaseRequestId = null;

            if (request.PurchaseRequestId.HasValue)
            {
                purchaseRequestId = new PurchaseRequestId(
                    request.PurchaseRequestId.Value);
            }

            var command = new CreatePurchaseOrderCommand(
                new VendorId(request.VendorId),
                request.Currency,
                purchaseRequestId);

            return await EndpointHelper.SendAsync<
                CreatePurchaseOrderCommand,
                PurchaseOrderId>(
                command,
                sender,
                cancellationToken,
                id => Results.Created(
                    $"/api/procurement/purchase-orders/{id.Value}",
                    id.Value));
        });
    }

    private static void MapGetPurchaseOrderById(
        RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPurchaseOrderByIdQuery(
                new PurchaseOrderId(id));

            return await EndpointHelper.SendAsync<
                GetPurchaseOrderByIdQuery,
                PurchaseOrderDto>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapGetPurchaseOrdersByVendor(
        RouteGroupBuilder group)
    {
        group.MapGet("/vendor/{vendorId:guid}", async (
            Guid vendorId,
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 20 : pageSize;

            var query = new GetPurchaseOrdersByVendorQuery(
                new VendorId(vendorId),
                pageNumber,
                pageSize);

            return await EndpointHelper.SendAsync<
                GetPurchaseOrdersByVendorQuery,
                PaginatedList<PurchaseOrderDto>>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapGetPurchaseOrdersByPurchaseRequest(
        RouteGroupBuilder group)
    {
        group.MapGet("/purchase-request/{requestId:guid}", async (
            Guid requestId,
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 20 : pageSize;

            var query = new GetPurchaseOrdersByPurchaseRequestQuery(
                new PurchaseRequestId(requestId),
                pageNumber,
                pageSize);

            return await EndpointHelper.SendAsync<
                GetPurchaseOrdersByPurchaseRequestQuery,
                PaginatedList<PurchaseOrderDto>>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapAddPurchaseOrderLine(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/lines", async (
            Guid id,
            OrderLine request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AddPurchaseOrderLineCommand(
                new PurchaseOrderId(id),
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

    private static void MapSubmitPurchaseOrder(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/submit", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new SubmitPurchaseOrderCommand(
                new PurchaseOrderId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapApprovePurchaseOrder(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/approve", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ApprovePurchaseOrderCommand(
                new PurchaseOrderId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapCancelPurchaseOrder(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/cancel", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CancelPurchaseOrderCommand(
                new PurchaseOrderId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapCompletePurchaseOrder(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/complete", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CompletePurchaseOrderCommand(
                new PurchaseOrderId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    public sealed record CreatePurchaseOrderRequest(
        Guid VendorId,
        string Currency = "USD",
        Guid? PurchaseRequestId = null);

    public sealed record OrderLine(
        string ItemName,
        int Quantity,
        decimal UnitPrice,
        string Currency = "USD");
}