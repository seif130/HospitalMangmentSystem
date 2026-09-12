using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractByIdQuery
{
    public sealed class GetVendorContractByIdHandler(IVendorContractRepository contracts) : IQueryHandler<GetVendorContractByIdQuery, VendorContractDto>
    {
        public async Task<Result<VendorContractDto>> Handle(GetVendorContractByIdQuery request, CancellationToken ct)
        {
            var contract = await contracts.GetByIdAsync(request.VendorContractId, ct);
            return contract is null ? Result.Failure<VendorContractDto>(Error.NotFound("VendorContract.NotFound", "Vendor contract was not found.")) : Result.Success(contract.ToDto());
        }
    }

}
