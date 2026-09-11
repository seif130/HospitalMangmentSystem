using HospitalSystem.Application.Models;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.ActivateVendorCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.DeactivateVendorCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorsQuery;
using HospitalSystem.WebApi.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static HospitalSystem.WebApi.Modules.Procurement.Endpoints.ProcurementEndpoints;

namespace HospitalSystem.Procurement.WebApi.Modules.Procurement.Endpoints.Vendors
{
    public static class VendorEndpoints
    {
        public static RouteGroupBuilder MapVendorEndpoints(
            this RouteGroupBuilder group)
        {
            var vendors = group
                .MapGroup("/vendors")
                .WithTags("Vendors");

            vendors.MapPost("", Create);
            vendors.MapGet("/{id:guid}", GetById);
            vendors.MapGet("", GetAll);
            vendors.MapPut("/{id:guid}", Rename);
            vendors.MapPut("/{id:guid}/contact", UpdateContact);
            vendors.MapPost("/{id:guid}/activate", Activate);
            vendors.MapPost("/{id:guid}/deactivate", Deactivate);

            return vendors;
        }

        private static async Task<IResult> Create(
            CreateVendorRequest body,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new CreateVendorCommand(
                    body.Name,
                    body.ContactEmail,
                    body.ContactPhone),
                sender,
                ct,
                id => Results.Created(
                    $"/api/procurement/vendors/{id.Value}",
                    id.Value));
        }

        private static async Task<IResult> GetById(
            Guid id,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync<
                GetVendorByIdQuery,
                VendorDto>(
                new(new VendorId(id)),
                sender,
                ct);
        }

        private static async Task<IResult> GetAll(
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync<
                GetVendorsQuery,
                PaginatedList<VendorDto>>(
                new(
                    pageNumber == 0 ? 1 : pageNumber,
                    pageSize == 0 ? 20 : pageSize),
                sender,
                ct);
        }

        private static async Task<IResult> Rename(
            Guid id,
            RenameVendorRequest body,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new RenameVendorCommand(
                    new VendorId(id),
                    body.Name),
                sender,
                ct);
        }

        private static async Task<IResult> UpdateContact(
            Guid id,
            UpdateContactRequest body,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new UpdateVendorContactCommand(
                    new VendorId(id),
                    body.ContactEmail,
                    body.ContactPhone),
                sender,
                ct);
        }

        private static async Task<IResult> Activate(
            Guid id,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new ActivateVendorCommand(new VendorId(id)),
                sender,
                ct);
        }

        private static async Task<IResult> Deactivate(
            Guid id,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new DeactivateVendorCommand(new VendorId(id)),
                sender,
                ct);
        }
    }
}
