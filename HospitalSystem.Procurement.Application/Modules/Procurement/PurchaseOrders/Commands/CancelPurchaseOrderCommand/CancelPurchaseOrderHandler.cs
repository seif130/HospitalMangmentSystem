
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CancelPurchaseOrderCommand
{
    public sealed class CancelPurchaseOrderHandler(IPurchaseOrderRepository orders, IProcurementUnitOfWork uow) : ICommandHandler<CancelPurchaseOrderCommand>
    {
        public async Task<Result> Handle(CancelPurchaseOrderCommand request, CancellationToken ct) => 
            await PurchaseOrderCommandHelper.Execute(request.PurchaseOrderId, orders, uow, x => x.Cancel(), ct);
    }

}
