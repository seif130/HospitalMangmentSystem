using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
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
