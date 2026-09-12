using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Commands.ApprovePurchaseOrderCommand
{
    public sealed record ApprovePurchaseOrderCommand(PurchaseOrderId PurchaseOrderId) : ICommand;

}
