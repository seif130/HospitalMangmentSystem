using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands
{
    internal static class PurchaseOrderCommandHelper
    {
        public static async Task<Result> Execute(PurchaseOrderId id, IPurchaseOrderRepository orders, IProcurementUnitOfWork uow, Action<PurchaseOrder> action, CancellationToken ct)
        {
            var entity = await orders.GetByIdAsync(id, ct);
            if (entity is null) 
                return Result.Failure(Error.NotFound("PurchaseOrder.NotFound", "Purchase order was not found."));
            action(entity);
            await uow.SaveChangesAsync(ct);
            return Result.Success();
        }
    }

}
