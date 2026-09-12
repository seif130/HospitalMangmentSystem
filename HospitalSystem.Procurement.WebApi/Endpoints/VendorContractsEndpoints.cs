using MediatR;
using HospitalSystem.WebApi.Common;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand;
using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractsByVendorQuery;
using HospitalSystem.Application.Models;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ActivateVendorContractCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ExpireVendorContractCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.TerminateVendorContractCommand;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enums;

namespace HospitalSystem.Procurement.WebApi.Endpoints;

public static class VendorContractsEndpoints
{
    public static void MapVendorContractsEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/vendor-contracts")
            .WithTags("Vendor Contracts");

        MapCreateVendorContract(group);
        MapGetVendorContractById(group);
        MapGetVendorContractsByVendor(group);
        MapActivateVendorContract(group);
        MapExpireVendorContract(group);
        MapTerminateVendorContract(group);
    }

    private static void MapCreateVendorContract(
        RouteGroupBuilder group)
    {
        group.MapPost("", async (
            CreateContractRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateVendorContractCommand(
                new VendorId(request.VendorId),
                request.Category,
                request.StartUtc,
                request.EndUtc,
                request.Amount,
                request.Currency);

            return await EndpointHelper.SendAsync<
                CreateVendorContractCommand,
                VendorContractId>(
                command,
                sender,
                cancellationToken,
                id => Results.Created(
                    $"/api/procurement/vendor-contracts/{id.Value}",
                    id.Value));
        });
    }

    private static void MapGetVendorContractById(
        RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetVendorContractByIdQuery(
                new VendorContractId(id));

            return await EndpointHelper.SendAsync<
                GetVendorContractByIdQuery,
                VendorContractDto>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapGetVendorContractsByVendor(
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

            var query = new GetVendorContractsByVendorQuery(
                new VendorId(vendorId),
                pageNumber,
                pageSize);

            return await EndpointHelper.SendAsync<
                GetVendorContractsByVendorQuery,
                PaginatedList<VendorContractDto>>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapActivateVendorContract(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/activate", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ActivateVendorContractCommand(
                new VendorContractId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapExpireVendorContract(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/expire", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ExpireVendorContractCommand(
                new VendorContractId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapTerminateVendorContract(
        RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/terminate", async (
            Guid id,
            TerminateRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new TerminateVendorContractCommand(
                new VendorContractId(id),
                request.Reason);

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    public sealed record CreateContractRequest(
        Guid VendorId,
        VendorServiceCategory Category,
        DateTime StartUtc,
        DateTime EndUtc,
        decimal Amount,
        string Currency = "USD");

    public sealed record TerminateRequest(
        string Reason);
}