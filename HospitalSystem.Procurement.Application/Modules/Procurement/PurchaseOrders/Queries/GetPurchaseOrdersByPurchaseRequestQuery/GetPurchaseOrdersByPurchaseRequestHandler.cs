using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByPurchaseRequestQuery
{
    public sealed class GetPurchaseOrdersByPurchaseRequestHandler(IPurchaseOrderRepository orders) : IQueryHandler<GetPurchaseOrdersByPurchaseRequestQuery, PaginatedList<PurchaseOrderDto>>
    {
        public async Task<Result<PaginatedList<PurchaseOrderDto>>> Handle(GetPurchaseOrdersByPurchaseRequestQuery request, CancellationToken ct)
        {
            var (items, total) = await orders.GetByPurchaseRequestAsync(request.PurchaseRequestId, request.PageNumber, request.PageSize, ct);
            return Result.Success(new PaginatedList<PurchaseOrderDto>(items.Select(x => x.ToDto()).ToList(), total, request.PageNumber, request.PageSize));
        }
    }

}
