using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Enums;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand
{
    public sealed class CreatePurchaseOrderHandler(IVendorRepository vendors, IPurchaseRequestRepository requests, IPurchaseOrderRepository orders, IProcurementUnitOfWork uow) : ICommandHandler<CreatePurchaseOrderCommand, PurchaseOrderId>
    {
        public async Task<Result<PurchaseOrderId>> Handle(CreatePurchaseOrderCommand request, CancellationToken ct)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId, ct);
            if (vendor is null)
                return Result.Failure<PurchaseOrderId>(Error.NotFound("Vendor.NotFound", "Vendor was not found."));
            if (vendor.Status != VendorStatus.Active) 
                return Result.Failure<PurchaseOrderId>(Error.Conflict("Vendor.Inactive", "Only active vendors can receive purchase orders."));
            if (request.PurchaseRequestId.HasValue)
            {
                var pr = await requests.GetByIdAsync(request.PurchaseRequestId.Value, ct);
                if (pr is null)
                    return Result.Failure<PurchaseOrderId>(Error.NotFound("PurchaseRequest.NotFound", "Purchase request was not found."));
                if (pr.Status != PurchaseRequestStatus.Approved) 
                    return Result.Failure<PurchaseOrderId>(Error.Conflict("PurchaseRequest.NotApproved", "A purchase order can only be linked to an approved purchase request."));
            }
            var order = PurchaseOrder.Create(request.VendorId, request.Currency, request.PurchaseRequestId);
            await orders.AddAsync(order, ct);
            await uow.SaveChangesAsync(ct);
            return Result.Success(order.Id);
        }
    }

}
