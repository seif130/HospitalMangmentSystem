using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.DTOs
{
    public sealed record PurchaseOrderDto(Guid Id, Guid VendorId, Guid? PurchaseRequestId, decimal TotalAmount, string Currency, PurchaseOrderStatus Status, IReadOnlyList<PurchaseOrderLineDto> Lines);

}
