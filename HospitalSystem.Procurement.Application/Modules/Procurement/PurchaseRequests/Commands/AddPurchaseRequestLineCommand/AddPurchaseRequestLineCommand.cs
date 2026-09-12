using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.AddPurchaseRequestLineCommand
{
    public sealed record AddPurchaseRequestLineCommand(PurchaseRequestId PurchaseRequestId, string ItemName, int Quantity, decimal EstimatedUnitPrice, string Currency = "USD") : ICommand;

}
