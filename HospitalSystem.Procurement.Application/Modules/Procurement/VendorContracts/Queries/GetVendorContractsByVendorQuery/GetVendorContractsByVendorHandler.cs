using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractsByVendorQuery
{
    public sealed class GetVendorContractsByVendorHandler(IVendorContractRepository contracts) : IQueryHandler<GetVendorContractsByVendorQuery, PaginatedList<VendorContractDto>>
    {
        public async Task<Result<PaginatedList<VendorContractDto>>> Handle(GetVendorContractsByVendorQuery request, CancellationToken ct)
        {
            var (items, total) = await contracts.GetByVendorAsync(request.VendorId, request.PageNumber, request.PageSize, ct);
            return Result.Success(new PaginatedList<VendorContractDto>(items.Select(x => x.ToDto()).ToList(), total, request.PageNumber, request.PageSize));
        }
    }

}
