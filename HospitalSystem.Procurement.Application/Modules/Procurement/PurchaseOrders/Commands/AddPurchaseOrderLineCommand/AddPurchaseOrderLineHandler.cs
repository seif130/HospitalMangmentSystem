using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.AddPurchaseOrderLineCommand
{
    public sealed class AddPurchaseOrderLineHandler(IPurchaseOrderRepository orders, IProcurementUnitOfWork uow) : ICommandHandler<AddPurchaseOrderLineCommand>
    {
        public async Task<Result> Handle(AddPurchaseOrderLineCommand request, CancellationToken ct)
        {
            var order = await orders.GetByIdAsync(request.PurchaseOrderId, ct);
            if (order is null)
                return Result.Failure(Error.NotFound("PurchaseOrder.NotFound", "Purchase order was not found."));
            order.AddLine(request.ItemName, request.Quantity, Money.Create(request.UnitPrice, request.Currency));
            await uow.SaveChangesAsync(ct);
            return Result.Success();
        }
    }

}
