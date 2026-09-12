using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand
{
    public sealed record CreatePurchaseOrderCommand(VendorId VendorId, string Currency = "USD", PurchaseRequestId? PurchaseRequestId = null) : ICommand<PurchaseOrderId>;

}
