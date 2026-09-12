using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestByIdQuery
{
    public sealed class GetPurchaseRequestByIdHandler(IPurchaseRequestRepository requests) : IQueryHandler<GetPurchaseRequestByIdQuery, PurchaseRequestDto>
    {
        public async Task<Result<PurchaseRequestDto>> Handle(GetPurchaseRequestByIdQuery request, CancellationToken ct)
        {
            var entity = await requests.GetByIdAsync(request.PurchaseRequestId, ct);
            return entity is null ? Result.Failure<PurchaseRequestDto>(Error.NotFound("PurchaseRequest.NotFound", "Purchase request was not found.")) : Result.Success(entity.ToDto());
        }
    }

}
