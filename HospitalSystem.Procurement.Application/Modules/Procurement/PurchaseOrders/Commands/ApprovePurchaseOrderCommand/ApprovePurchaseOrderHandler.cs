
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.ApprovePurchaseOrderCommand
{
    public sealed class ApprovePurchaseOrderHandler(IPurchaseOrderRepository orders, IProcurementUnitOfWork uow) : ICommandHandler<ApprovePurchaseOrderCommand>
    {
        public async Task<Result> Handle(ApprovePurchaseOrderCommand request, CancellationToken ct) => 
            await PurchaseOrderCommandHelper.Execute(request.PurchaseOrderId, orders, uow, x => x.Approve(), ct);
    }

}
