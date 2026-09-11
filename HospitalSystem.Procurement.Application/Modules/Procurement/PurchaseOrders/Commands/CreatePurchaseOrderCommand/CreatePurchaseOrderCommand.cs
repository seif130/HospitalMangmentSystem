using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand
{
    public sealed record CreatePurchaseOrderCommand(VendorId VendorId, string Currency = "USD", PurchaseRequestId? PurchaseRequestId = null) : ICommand<PurchaseOrderId>;

}
