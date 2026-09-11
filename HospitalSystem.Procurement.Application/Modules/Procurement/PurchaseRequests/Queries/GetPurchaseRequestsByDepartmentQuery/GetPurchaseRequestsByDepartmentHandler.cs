using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestsByDepartmentQuery
{
    public sealed class GetPurchaseRequestsByDepartmentHandler(IPurchaseRequestRepository requests) : IQueryHandler<GetPurchaseRequestsByDepartmentQuery, PaginatedList<PurchaseRequestDto>>
    {
        public async Task<Result<PaginatedList<PurchaseRequestDto>>> Handle(GetPurchaseRequestsByDepartmentQuery request, CancellationToken ct)
        {
            var (items, total) = await requests.GetByDepartmentAsync(request.DepartmentId, request.PageNumber, request.PageSize, ct);
            return Result.Success(new PaginatedList<PurchaseRequestDto>(items.Select(x => x.ToDto()).ToList(), total, request.PageNumber, request.PageSize));
        }
    }

}
