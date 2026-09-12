using MediatR;
using HospitalSystem.WebApi.Common;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Domain.identfires;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorsQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand;
using HospitalSystem.Application.Models;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.ActivateVendorCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.DeactivateVendorCommand;

namespace HospitalSystem.Procurement.WebApi.Endpoints;

public static class VendorsEndpoints
{
    public static void MapVendorsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/vendors")
            .WithTags("Vendors");

        MapCreateVendor(group);
        MapGetVendorById(group);
        MapGetVendors(group);
        MapRenameVendor(group);
        MapUpdateVendorContact(group);
        MapActivateVendor(group);
        MapDeactivateVendor(group);
    }

    private static void MapCreateVendor(RouteGroupBuilder group)
    {
        group.MapPost("", async (
            CreateVendorRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateVendorCommand(
                request.Name,
                request.ContactEmail,
                request.ContactPhone);

            return await EndpointHelper.SendAsync<
                CreateVendorCommand,
                VendorId>(
                command,
                sender,
                cancellationToken,
                id => Results.Created(
                    $"/api/procurement/vendors/{id.Value}",
                    id.Value));
        });
    }

    private static void MapGetVendorById(RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetVendorByIdQuery(
                new VendorId(id));

            return await EndpointHelper.SendAsync<
                GetVendorByIdQuery,
                VendorDto>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapGetVendors(RouteGroupBuilder group)
    {
        group.MapGet("", async (
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 20 : pageSize;

            var query = new GetVendorsQuery(
                pageNumber,
                pageSize);

            return await EndpointHelper.SendAsync<
                GetVendorsQuery,
                PaginatedList<VendorDto>>(
                query,
                sender,
                cancellationToken);
        });
    }

    private static void MapRenameVendor(RouteGroupBuilder group)
    {
        group.MapPut("/{id:guid}", async (
            Guid id,
            RenameVendorRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RenameVendorCommand(
                new VendorId(id),
                request.Name);

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapUpdateVendorContact(RouteGroupBuilder group)
    {
        group.MapPut("/{id:guid}/contact", async (
            Guid id,
            UpdateContactRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateVendorContactCommand(
                new VendorId(id),
                request.ContactEmail,
                request.ContactPhone);

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapActivateVendor(RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/activate", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ActivateVendorCommand(
                new VendorId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    private static void MapDeactivateVendor(RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/deactivate", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new DeactivateVendorCommand(
                new VendorId(id));

            return await EndpointHelper.SendAsync(
                command,
                sender,
                cancellationToken);
        });
    }

    public sealed record CreateVendorRequest(
        string Name,
        string? ContactEmail,
        string? ContactPhone);

    public sealed record RenameVendorRequest(
        string Name);

    public sealed record UpdateContactRequest(
        string? ContactEmail,
        string? ContactPhone);
}