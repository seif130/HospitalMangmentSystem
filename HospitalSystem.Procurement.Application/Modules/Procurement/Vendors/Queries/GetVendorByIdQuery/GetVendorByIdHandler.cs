using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorByIdQuery
{
    public sealed class GetVendorByIdHandler(IVendorRepository vendors) : IQueryHandler<GetVendorByIdQuery, VendorDto>
    {
        public async Task<Result<VendorDto>> Handle(GetVendorByIdQuery request, CancellationToken ct)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId, ct);
            return vendor is null ? Result.Failure<VendorDto>(Error.NotFound("Vendor.NotFound", "Vendor was not found.")) : Result.Success(vendor.ToDto());
        }
    }

}
