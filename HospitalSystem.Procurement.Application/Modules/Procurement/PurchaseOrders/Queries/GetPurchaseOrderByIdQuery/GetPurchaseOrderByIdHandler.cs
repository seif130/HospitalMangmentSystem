using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Application.Modules.Procurement.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrderByIdQuery
{
    public sealed class GetPurchaseOrderByIdHandler(IPurchaseOrderRepository orders) : IQueryHandler<GetPurchaseOrderByIdQuery, PurchaseOrderDto>
    {
        public async Task<Result<PurchaseOrderDto>> Handle(GetPurchaseOrderByIdQuery request, CancellationToken ct)
        {
            var entity = await orders.GetByIdAsync(request.PurchaseOrderId, ct);
            return entity is null ? Result.Failure<PurchaseOrderDto>(Error.NotFound("PurchaseOrder.NotFound", "Purchase order was not found.")) : Result.Success(entity.ToDto());
        }
    }

}
