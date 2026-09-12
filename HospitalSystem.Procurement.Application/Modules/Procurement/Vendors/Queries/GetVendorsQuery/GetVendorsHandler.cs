using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorsQuery
{
    public sealed class GetVendorsHandler(IVendorRepository vendors) : IQueryHandler<GetVendorsQuery, PaginatedList<VendorDto>>
    {
        public async Task<Result<PaginatedList<VendorDto>>> Handle(GetVendorsQuery request, CancellationToken ct)
        {
            var (items, total) = await vendors.GetPagedAsync(request.PageNumber, request.PageSize, ct);
            var dto = new PaginatedList<VendorDto>(items.Select(x => x.ToDto()).ToList(), total, request.PageNumber, request.PageSize);
            return Result.Success(dto);
        }
    }

}
