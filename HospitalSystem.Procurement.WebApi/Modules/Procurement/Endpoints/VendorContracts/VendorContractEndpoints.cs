using HospitalSystem.Application.Models;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ActivateVendorContractCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ExpireVendorContractCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.TerminateVendorContractCommand;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractByIdQuery;
using HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractsByVendorQuery;
using HospitalSystem.WebApi.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static HospitalSystem.WebApi.Modules.Procurement.Endpoints.ProcurementEndpoints;

namespace HospitalSystem.Procurement.WebApi.Modules.Procurement.Endpoints.VendorContracts
{
    public static class VendorContractEndpoints
    {
        public static RouteGroupBuilder MapVendorContractEndpoints(
            this RouteGroupBuilder group)
        {
            var contracts = group
                .MapGroup("/vendor-contracts")
                .WithTags("Vendor Contracts");

            contracts.MapPost("", Create);
            contracts.MapGet("/{id:guid}", GetById);
            contracts.MapGet("/vendor/{vendorId:guid}", GetByVendor);
            contracts.MapPost("/{id:guid}/activate", Activate);
            contracts.MapPost("/{id:guid}/expire", Expire);
            contracts.MapPost("/{id:guid}/terminate", Terminate);

            return contracts;
        }

        private static async Task<IResult> Create(
            CreateContractRequest body,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new CreateVendorContractCommand(
                    new VendorId(body.VendorId),
                    body.Category,
                    body.StartUtc,
                    body.EndUtc,
                    body.Amount,
                    body.Currency),
                sender,
                ct,
                id => Results.Created(
                    $"/api/procurement/vendor-contracts/{id.Value}",
                    id.Value));
        }

        private static async Task<IResult> GetById(
            Guid id,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync<
                GetVendorContractByIdQuery,
                VendorContractDto>(
                new(new VendorContractId(id)),
                sender,
                ct);
        }

        private static async Task<IResult> GetByVendor(
            Guid vendorId,
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync<
                GetVendorContractsByVendorQuery,
                PaginatedList<VendorContractDto>>(
                new(
                    new VendorId(vendorId),
                    pageNumber == 0 ? 1 : pageNumber,
                    pageSize == 0 ? 20 : pageSize),
                sender,
                ct);
        }

        private static async Task<IResult> Activate(
            Guid id,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new ActivateVendorContractCommand(
                    new VendorContractId(id)),
                sender,
                ct);
        }

        private static async Task<IResult> Expire(
            Guid id,
            DateTime? asOfUtc,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new ExpireVendorContractCommand(
                    new VendorContractId(id),
                    asOfUtc ?? DateTime.UtcNow),
                sender,
                ct);
        }

        private static async Task<IResult> Terminate(
            Guid id,
            TerminateRequest body,
            ISender sender,
            CancellationToken ct)
        {
            return await EndpointHelper.SendAsync(
                new TerminateVendorContractCommand(
                    new VendorContractId(id),
                    body.Reason),
                sender,
                ct);
        }
    }
}
