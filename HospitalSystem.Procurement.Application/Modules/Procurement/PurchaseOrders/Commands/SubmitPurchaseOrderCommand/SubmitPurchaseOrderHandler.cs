using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.SubmitPurchaseOrderCommand
{
    public sealed class SubmitPurchaseOrderHandler(IPurchaseOrderRepository orders, IProcurementUnitOfWork uow) : ICommandHandler<SubmitPurchaseOrderCommand>
    {
        public async Task<Result> Handle(SubmitPurchaseOrderCommand request, CancellationToken ct) =>
            await PurchaseOrderCommandHelper.Execute(request.PurchaseOrderId, orders, uow, x => x.Submit(), ct);
    }

}
