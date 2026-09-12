using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand
{
    public sealed record RejectPurchaseRequestCommand(PurchaseRequestId PurchaseRequestId, string Reason) : ICommand;

}
